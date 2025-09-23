using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using SmartStore.Core.Infrastructure.DependencyManagement;
using SmartStore.Utilities;
using SmartStore.Utilities.Threading;

namespace SmartStore.Core.Caching
{
    public partial class MemoryCacheManager : DisposableObject, ICacheManager
    {
        const string LockRecursionExceptionMessage = "Acquiring identical cache items recursively is not supported. Key: {0}";

        // We put a special string into cache if value is null,
        // otherwise our 'Contains()' would always return false,
        // which is bad if we intentionally wanted to save NULL values.
        public const string FakeNull = "__[NULL]__";

        private readonly Work<ICacheScopeAccessor> _scopeAccessor;
        private IMemoryCache _cache;

        public MemoryCacheManager(Work<ICacheScopeAccessor> scopeAccessor, IMemoryCache memoryCache = null)
        {
            _scopeAccessor = scopeAccessor;
            _cache = memoryCache ?? CreateCache();
        }

        private IMemoryCache CreateCache()
        {
            var options = Options.Create(new MemoryCacheOptions());
            return new MemoryCache(options);
        }

        public bool IsDistributedCache => false;

        private bool TryGet<T>(string key, bool independent, out T value)
        {
            value = default(T);

            if (_cache.TryGetValue(key, out object obj))
            {
                // Make the parent scope's entry depend on this
                if (!independent)
                {
                    var scope = _scopeAccessor.Value.Current;
                    if (scope != null)
                    {
                        scope.AddDependency(key);
                    }
                }

                if (obj.Equals(FakeNull))
                {
                    value = default(T);
                    return true;
                }

                value = (T)obj;
                return true;
            }

            return false;
        }

        public T Get<T>(string key, bool independent = false)
        {
            TryGet<T>(key, independent, out T value);
            return value;
        }

        public T Get<T>(string key, Func<T> acquirer, TimeSpan? duration = null, bool independent = false, bool allowRecursion = false)
        {
            if (TryGet<T>(key, independent, out T value))
            {
                return value;
            }

            using (KeyedLock.Lock("cache:" + key))
            {
                if (TryGet<T>(key, independent, out value))
                {
                    return value;
                }

                value = acquirer();
                Set(key, value, duration, independent);
                return value;
            }
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> acquirer, TimeSpan? duration = null, bool independent = false, bool allowRecursion = false)
        {
            if (TryGet<T>(key, independent, out T value))
            {
                return value;
            }

            using (await AsyncLock.KeyedAsync("cache:" + key))
            {
                if (TryGet<T>(key, independent, out value))
                {
                    return value;
                }

                value = await acquirer();
                Set(key, value, duration, independent);
                return value;
            }
        }

        public void Set(string key, object value, TimeSpan? duration, bool independent = false)
        {
            var options = new MemoryCacheEntryOptions();
            
            if (duration.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = duration.Value;
            }

            var valueToCache = value ?? FakeNull;
            _cache.Set(key, valueToCache, options);

            if (!independent)
            {
                var scope = _scopeAccessor.Value.Current;
                if (scope != null)
                {
                    scope.AddDependency(key);
                }
            }
        }

        public bool Contains(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public ISet GetHashSet(string key, Func<IEnumerable<string>> acquirer = null)
        {
            // Simple implementation - return a HashSet wrapper
            if (!_cache.TryGetValue(key, out object value))
            {
                var hashSet = new HashSet<string>();
                if (acquirer != null)
                {
                    foreach (var item in acquirer())
                    {
                        hashSet.Add(item);
                    }
                }
                _cache.Set(key, hashSet);
                return new HashSetWrapper(hashSet);
            }
            return new HashSetWrapper((HashSet<string>)value);
        }

        public void Put(string key, object value, TimeSpan? duration = null, IEnumerable<string> dependencies = null)
        {
            Set(key, value, duration, false);
        }

        public IEnumerable<string> Keys(string pattern)
        {
            // Note: MemoryCache doesn't expose keys directly
            // This is a limitation - would need to maintain separate key collection
            return new List<string>();
        }

        public int RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<string>();

            // Note: In .NET Core MemoryCache, we can't easily enumerate keys
            // This is a limitation compared to System.Runtime.Caching.MemoryCache
            // You might need to maintain a separate collection of keys if pattern removal is critical
            
            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
            }
            
            return keysToRemove.Count;
        }

        public void Clear()
        {
            if (_cache is MemoryCache mc)
            {
                mc.Dispose();
                _cache = CreateCache();
            }
        }

        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                _cache?.Dispose();
            }
        }
    }

    public class HashSetWrapper : ISet
    {
        private readonly HashSet<string> _hashSet;

        public HashSetWrapper(HashSet<string> hashSet)
        {
            _hashSet = hashSet;
        }

        public bool Add(string item) => _hashSet.Add(item);
        public void AddRange(IEnumerable<string> items)
        {
            foreach (var item in items)
                _hashSet.Add(item);
        }
        public bool Remove(string item) => _hashSet.Remove(item);
        public bool Contains(string item) => _hashSet.Contains(item);
        public void Clear() => _hashSet.Clear();
        public int Count => _hashSet.Count;
        
        public bool Move(string destinationKey, string item)
        {
            // Simple implementation - just remove from this set
            return _hashSet.Remove(item);
        }

        public long UnionWith(params string[] keys)
        {
            // Simple implementation - add all keys
            var count = 0L;
            foreach (var key in keys)
            {
                if (_hashSet.Add(key))
                    count++;
            }
            return count;
        }

        public long IntersectWith(params string[] keys)
        {
            // Simple implementation
            var keysSet = new HashSet<string>(keys);
            _hashSet.IntersectWith(keysSet);
            return _hashSet.Count;
        }

        public long ExceptWith(params string[] keys)
        {
            var count = 0L;
            foreach (var key in keys)
            {
                if (_hashSet.Remove(key))
                    count++;
            }
            return count;
        }

        public IEnumerator<string> GetEnumerator() => _hashSet.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
