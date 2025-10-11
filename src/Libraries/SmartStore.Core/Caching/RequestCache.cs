using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartStore.Core.Caching;

// TODO: Migrate to ASP.NET Core
// This file uses HttpContextBase
// Use IHttpContextAccessor and HttpContext.Items

namespace SmartStore.Core.Caching
{
    public class RequestCache : ICacheManager
    {
        public bool IsDistributedCache => false;
        
        public T Get<T>(string key, bool independent = false) => default(T);
        
        public T Get<T>(string key, Func<T> acquirer, TimeSpan? duration = null, bool independent = false, bool allowRecursion = false) 
            => acquirer != null ? acquirer() : default(T);
        
        public Task<T> GetAsync<T>(string key, Func<Task<T>> acquirer, TimeSpan? duration = null, bool independent = false, bool allowRecursion = false) 
            => acquirer != null ? acquirer() : Task.FromResult(default(T));
        
        public ISet GetHashSet(string key, Func<IEnumerable<string>> acquirer = null) 
            => new HashSet();
        
        public void Put(string key, object value, TimeSpan? duration = null, IEnumerable<string> dependencies = null) { }
        
        public bool Contains(string key) => false;
        
        public void Remove(string key) { }
        
        public IEnumerable<string> Keys(string pattern) => new List<string>();
        
        public int RemoveByPattern(string pattern) => 0;
        
        public void Clear() { }
    }
    
    public class HashSet : ISet
    {
        public bool Add(string item) => false;
        public void AddRange(IEnumerable<string> items) { }
        public void Clear() { }
        public bool Contains(string item) => false;
        public bool Remove(string item) => false;
        public bool Move(string destinationKey, string item) => false;
        public long UnionWith(params string[] keys) => 0;
        public long IntersectWith(params string[] keys) => 0;
        public long ExceptWith(params string[] keys) => 0;
        public int Count => 0;
        public IEnumerator<string> GetEnumerator() => new List<string>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
