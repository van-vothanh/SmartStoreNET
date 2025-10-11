using System;
using SmartStore.Core.Caching;
// TODO: Migrate to IMemoryCache
// This file uses System.Runtime.Caching.MemoryCache

namespace SmartStore.Core.Caching
{
    // TODO: Migrate to Microsoft.Extensions.Caching.Memory.IMemoryCache
    // Use dependency injection instead of MemoryCache.Default
    /*
    Original implementation commented out for migration
    */
    
    public partial class MemoryCacheManager : ICacheManager
    {
        public bool IsDistributedCache => false;
        public T Get<T>(string key, bool independent = false) => default(T);
        public T Get<T>(string key, Func<T> acquirer, System.TimeSpan? duration = null, bool independent = false, bool allowRecursion = false) 
            => acquirer != null ? acquirer() : default(T);
        public System.Threading.Tasks.Task<T> GetAsync<T>(string key, Func<System.Threading.Tasks.Task<T>> acquirer, System.TimeSpan? duration = null, bool independent = false, bool allowRecursion = false) 
            => acquirer != null ? acquirer() : System.Threading.Tasks.Task.FromResult(default(T));
        public ISet GetHashSet(string key, Func<System.Collections.Generic.IEnumerable<string>> acquirer = null) 
            => new RequestCache.HashSet();
        public void Put(string key, object value, System.TimeSpan? duration = null, System.Collections.Generic.IEnumerable<string> dependencies = null) { }
        public bool Contains(string key) => false;
        public void Remove(string key) { }
        public System.Collections.Generic.IEnumerable<string> Keys(string pattern) => new System.Collections.Generic.List<string>();
        public int RemoveByPattern(string pattern) => 0;
        public void Clear() { }
    }
}
