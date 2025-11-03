using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using SmartStore.Core.Infrastructure.DependencyManagement;

namespace SmartStore.Core.Caching
{
    public partial class MemoryCacheManager : ICacheManager
    {
        public const string FakeNull = "__[NULL]__";

        private readonly IMemoryCache _cache;
        private readonly Work<ICacheScopeAccessor> _scopeAccessor;

        public MemoryCacheManager(IMemoryCache cache, Work<ICacheScopeAccessor> scopeAccessor)
        {
            _cache = cache;
            _scopeAccessor = scopeAccessor;
        }

        public bool IsDistributedCache => false;

        public T Get<T>(string key, bool independent = false)
        {
            return _cache.TryGetValue(key, out T value) ? value : default(T);
        }

        public T Get<T>(string key, Func<T> acquirer, TimeSpan? duration = null, bool independent = false, bool allowRecursion = false)
        {
            if (_cache.TryGetValue(key, out T value))
            {
                return value;
            }

            value = acquirer();
            Set(key, value, duration.HasValue ? (int)duration.Value.TotalMinutes : (int?)null);
            return value;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> acquirer, TimeSpan? duration = null, bool independent = false, bool allowRecursion = false)
        {
            if (_cache.TryGetValue(key, out T value))
            {
                return value;
            }

            value = await acquirer();
            Set(key, value, duration.HasValue ? (int)duration.Value.TotalMinutes : (int?)null);
            return value;
        }

        public ISet GetHashSet(string key, Func<IEnumerable<string>> acquirer = null)
        {
            throw new NotImplementedException("HashSet not implemented in this cache manager");
        }

        public void Put(string key, object value, TimeSpan? duration = null, IEnumerable<string> dependencies = null)
        {
            Set(key, value, duration.HasValue ? (int)duration.Value.TotalMinutes : (int?)null);
        }

        public void Set(string key, object value, int? cacheTimeInMinutes = null)
        {
            var options = new MemoryCacheEntryOptions();
            if (cacheTimeInMinutes.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTimeInMinutes.Value);
            }
            _cache.Set(key, value ?? FakeNull, options);
        }

        public bool Contains(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public IEnumerable<string> Keys(string pattern)
        {
            // IMemoryCache doesn't support key enumeration
            // This would need to be tracked separately
            return Enumerable.Empty<string>();
        }

        public int RemoveByPattern(string pattern)
        {
            // IMemoryCache doesn't support pattern-based removal
            // This would need to be tracked separately
            return 0;
        }

        public void Clear()
        {
            // IMemoryCache doesn't have a Clear method
            // Would need to track keys separately
        }
    }
}
