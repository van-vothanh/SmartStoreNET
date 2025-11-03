// Compatibility stub for System.Web.Caching.Cache
// TODO: Replace with Microsoft.Extensions.Caching.Memory

using Microsoft.Extensions.Caching.Memory;

namespace System.Web.Caching
{
    public class Cache
    {
        private readonly IMemoryCache _cache;
        
        public Cache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }
        
        public object this[string key]
        {
            get => _cache.TryGetValue(key, out var value) ? value : null;
            set => _cache.Set(key, value);
        }
        
        public object Get(string key) => this[key];
        
        public void Insert(string key, object value)
        {
            _cache.Set(key, value);
        }
        
        public void Insert(string key, object value, CacheDependency dependencies)
        {
            _cache.Set(key, value);
        }
        
        public object Remove(string key)
        {
            var value = this[key];
            _cache.Remove(key);
            return value;
        }
    }
    
    public class CacheDependency
    {
    }
}
