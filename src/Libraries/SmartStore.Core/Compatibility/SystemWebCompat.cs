// Minimal compatibility types for migration
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace System.Web
{
    public class PreApplicationStartMethodAttribute : Attribute
    {
        public PreApplicationStartMethodAttribute() { }
    }
    
    public interface IRegisteredObject
    {
        void Stop(bool immediate);
    }
    
    public class HttpRequest { }
    public abstract class HttpContextBase { }
}

namespace System.Web.Mvc
{
    public class AllowHtmlAttribute : Attribute { }
    public abstract class Controller { }
    public class GlobalFilterCollection : List<object> { }
    
    namespace Filters
    {
        // Placeholder for MVC filters
    }
}

namespace System.Web.Hosting
{
    public interface IRegisteredObject
    {
        void Stop(bool immediate);
    }
}

namespace System.Web.Routing
{
    public class RouteValueDictionary : Dictionary<string, object>
    {
        public RouteValueDictionary() : base() { }
        public RouteValueDictionary(object values) : base() { }
    }
    
    public class RouteData
    {
        public RouteValueDictionary Values { get; set; } = new RouteValueDictionary();
    }
    
    public abstract class RouteBase { }
}

namespace System.Web.Caching
{
    public class Cache { }
    public class CacheDependency { }
}

namespace System.Web.UI
{
    public class HtmlTextWriter : System.IO.TextWriter
    {
        public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;
    }
}

namespace System.Web.Security
{
    // Placeholder for security types
}

namespace System.Web.SessionState
{
    // Placeholder for session state types
}

namespace System.Web.Configuration
{
    // Placeholder for configuration types
}

namespace System.Web.Compilation
{
    // Placeholder for compilation types
}

namespace System.Data.Entity
{
    public class IndexAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsUnique { get; set; }
        public int Order { get; set; }
        
        public IndexAttribute() { }
        public IndexAttribute(string name) { Name = name; }
        public IndexAttribute(string name, int order) { Name = name; Order = order; }
    }
    
    public enum EntityState
    {
        Detached = 1,
        Unchanged = 2,
        Added = 4,
        Deleted = 8,
        Modified = 16
    }
    
    public class DbEntityEntry<T> where T : class
    {
        public T Entity { get; set; }
        public EntityState State { get; set; }
    }
    
    public class DbEntityEntry
    {
        public object Entity { get; set; }
        public EntityState State { get; set; }
    }
    
    public class DbSet<T> : IQueryable<T>, IEnumerable<T> where T : class
    {
        public Type ElementType => typeof(T);
        public Expression Expression => Expression.Constant(this);
        public IQueryProvider Provider => new MockQueryProvider();
        
        public IEnumerator<T> GetEnumerator() => new List<T>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    internal class MockQueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression) => throw new NotImplementedException();
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => throw new NotImplementedException();
        public object Execute(Expression expression) => throw new NotImplementedException();
        public TResult Execute<TResult>(Expression expression) => throw new NotImplementedException();
    }
}

namespace System.Data.Entity.Infrastructure
{
    // Placeholder for EF infrastructure types
}

namespace System.Data.SqlServerCe
{
    // Placeholder for SQL Server CE types
}

namespace System.Runtime.Caching
{
    public class MemoryCache : IDisposable
    {
        private static readonly MemoryCache _default = new MemoryCache();
        public static MemoryCache Default => _default;
        
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();
        
        public object this[string key] 
        { 
            get => _cache.TryGetValue(key, out var value) ? value : null;
            set => _cache[key] = value;
        }
        
        public void Dispose() => _cache.Clear();
        public object Get(string key) => this[key];
        public void Set(string key, object value, CacheItemPolicy policy) => this[key] = value;
    }
    
    public class CacheItemPolicy
    {
        public DateTimeOffset AbsoluteExpiration { get; set; }
        public TimeSpan SlidingExpiration { get; set; }
    }
}

namespace Autofac.Integration.Web
{
    public interface ILifetimeScopeProvider
    {
        ILifetimeScope ApplicationContainer { get; }
        ILifetimeScope RequestLifetime { get; }
    }
}

namespace Autofac.Integration.Mvc
{
    // Placeholder for Autofac MVC integration
}

namespace Microsoft.Web.Infrastructure
{
    public class DynamicModuleHelper
    {
        // Placeholder for dynamic module helper
    }
}

namespace AngleSharp.Extensions
{
    // Placeholder for AngleSharp extensions
}

namespace AngleSharp.Parser.Html
{
    public class HtmlParser
    {
        // Placeholder for AngleSharp HTML parser
    }
}

namespace Ganss.XSS
{
    public class HtmlSanitizer
    {
        public string Sanitize(string html) => html;
    }
}
