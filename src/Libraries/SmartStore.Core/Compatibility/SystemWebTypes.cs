using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace System.Web
{
    public abstract class HttpContextBase
    {
        public abstract HttpRequestBase Request { get; }
        public abstract HttpResponseBase Response { get; }
        public abstract HttpSessionStateBase Session { get; }
        public abstract IDictionary<object, object> Items { get; }
        public abstract bool IsDebuggingEnabled { get; }
        public abstract Exception Error { get; }
        public abstract string MapPath(string virtualPath);
    }

    public abstract class HttpRequestBase
    {
        public abstract string UserAgent { get; }
        public abstract string UserHostAddress { get; }
        public abstract Uri Url { get; }
        public abstract string HttpMethod { get; }
        public abstract System.Collections.Specialized.NameValueCollection Headers { get; }
        public abstract System.Collections.Specialized.NameValueCollection QueryString { get; }
        public abstract System.Collections.Specialized.NameValueCollection Form { get; }
        public abstract HttpFileCollectionBase Files { get; }
        public abstract bool IsAuthenticated { get; }
        public abstract bool IsSecureConnection { get; }
        public abstract string RawUrl { get; }
        public abstract Stream InputStream { get; }
    }

    public abstract class HttpResponseBase
    {
        public abstract int StatusCode { get; set; }
        public abstract string StatusDescription { get; set; }
        public abstract System.Collections.Specialized.NameValueCollection Headers { get; }
        public abstract void Write(string s);
        public abstract void Redirect(string url);
        public abstract void End();
        public abstract string ContentType { get; set; }
    }

    public abstract class HttpSessionStateBase
    {
        public abstract object this[string name] { get; set; }
        public abstract void Add(string name, object value);
        public abstract void Remove(string name);
        public abstract void Clear();
        public abstract string SessionID { get; }
    }

    public abstract class HttpFileCollectionBase
    {
        public abstract int Count { get; }
        public abstract HttpPostedFileBase this[int index] { get; }
        public abstract HttpPostedFileBase this[string name] { get; }
    }

    public abstract class HttpPostedFileBase
    {
        public abstract string FileName { get; }
        public abstract int ContentLength { get; }
        public abstract string ContentType { get; }
        public abstract Stream InputStream { get; }
    }

    // PreApplicationStartMethod attribute replacement
    [AttributeUsage(AttributeTargets.Method)]
    public class PreApplicationStartMethodAttribute : Attribute
    {
        public PreApplicationStartMethodAttribute() { }
    }
}

namespace System.Web.UI
{
    public class HtmlTextWriter : TextWriter
    {
        private TextWriter _writer;
        
        public HtmlTextWriter(TextWriter writer)
        {
            _writer = writer;
        }

        public override Encoding Encoding => _writer.Encoding;

        public override void Write(char value) => _writer.Write(value);
        public override void Write(string value) => _writer.Write(value);

        public void WriteAttribute(string name, string value)
        {
            Write($" {name}=\"{value}\"");
        }

        public void WriteBeginTag(string tagName)
        {
            Write($"<{tagName}");
        }

        public void WriteEndTag(string tagName)
        {
            Write($"</{tagName}>");
        }

        public void WriteFullBeginTag(string tagName)
        {
            Write($"<{tagName}>");
        }
    }
}

namespace System.Web.SessionState
{
    public enum SessionStateBehavior
    {
        Default,
        Required,
        ReadOnly,
        Disabled
    }

    public interface IHttpSessionState
    {
        object this[string name] { get; set; }
        string SessionID { get; }
        void Add(string name, object value);
        void Remove(string name);
        void Clear();
    }
}

namespace System.Web.Mvc.Filters
{
    public class FilterAttribute : Attribute
    {
    }
}

namespace System.Web.Hosting
{
    public static class HostingEnvironment
    {
        public static bool IsHosted => true;
        public static string ApplicationPhysicalPath => AppDomain.CurrentDomain.BaseDirectory;
    }

    public interface IRegisteredObject
    {
        void Stop(bool immediate);
    }
}

namespace System.Web.Compilation
{
    public static class BuildManager
    {
        public static void AddReferencedAssembly(System.Reflection.Assembly assembly) { }
    }
}

namespace System.Web.Caching
{
    public class Cache
    {
        private Dictionary<string, object> _cache = new Dictionary<string, object>();
        
        public object this[string key] 
        { 
            get => _cache.TryGetValue(key, out var value) ? value : null;
            set => _cache[key] = value;
        }
        
        public void Insert(string key, object value) => _cache[key] = value;
        public void Remove(string key) => _cache.Remove(key);
    }
}

namespace System.Web.Security
{
    public static class FormsAuthentication
    {
        public static void SignOut() { }
    }
}

namespace System.Web.Routing
{
    public class RouteValueDictionary : Dictionary<string, object>
    {
        public RouteValueDictionary() { }
        public RouteValueDictionary(object values) { }
    }

    public class RouteData
    {
        public RouteValueDictionary Values { get; set; } = new RouteValueDictionary();
    }
}

namespace System.Runtime.Remoting.Messaging
{
    public static class CallContext
    {
        private static readonly ThreadLocal<Dictionary<string, object>> _data = 
            new ThreadLocal<Dictionary<string, object>>(() => new Dictionary<string, object>());

        public static void SetData(string name, object data)
        {
            _data.Value[name] = data;
        }

        public static object GetData(string name)
        {
            return _data.Value.TryGetValue(name, out var value) ? value : null;
        }
    }
}

namespace System.Runtime.Caching
{
    public class MemoryCache
    {
        private Dictionary<string, object> _cache = new Dictionary<string, object>();
        
        public static MemoryCache Default { get; } = new MemoryCache();
        
        public object this[string key] 
        { 
            get => _cache.TryGetValue(key, out var value) ? value : null;
            set => _cache[key] = value;
        }
        
        public void Set(string key, object value, DateTimeOffset expiration) => _cache[key] = value;
        public void Remove(string key) => _cache.Remove(key);
    }

    public class CacheItemPolicy
    {
        public DateTimeOffset AbsoluteExpiration { get; set; }
        public TimeSpan SlidingExpiration { get; set; }
    }
}

namespace Microsoft.EntityFrameworkCore
{
    public class DbSet<T> where T : class
    {
    }
}

namespace NuGet
{
    public class PackageBuilder
    {
    }
}

namespace Microsoft.Web.Infrastructure.DynamicModuleHelper
{
    public static class DynamicModuleUtility
    {
        public static void RegisterModule(Type moduleType) { }
    }
}

namespace System.Data.SqlServerCe
{
    public class SqlCeConnection
    {
    }
}

namespace Autofac.Integration.Mvc
{
    public class AutofacDependencyResolver
    {
    }
}

namespace Autofac.Integration.Web
{
    public interface IContainerProvider
    {
        IContainer ApplicationContainer { get; }
        ILifetimeScope RequestLifetime { get; }
    }
}

namespace System.Data.Entity.Core
{
    public class EntityException : Exception
    {
        public EntityException() { }
        public EntityException(string message) : base(message) { }
        public EntityException(string message, Exception innerException) : base(message, innerException) { }
    }

    namespace Objects
    {
        public class ObjectContext
        {
        }
    }
}

namespace System.Data.Entity.Infrastructure
{
    public enum DbEntityState
    {
        Detached = 1,
        Unchanged = 2,
        Added = 4,
        Deleted = 8,
        Modified = 16
    }

    public class DbEntityEntry
    {
        public object Entity { get; set; }
        public DbEntityState State { get; set; }
    }
}

namespace System.Data.Entity
{
    public enum EntityState
    {
        Detached = 1,
        Unchanged = 2,
        Added = 4,
        Deleted = 8,
        Modified = 16
    }
}

namespace AngleSharp.Extensions
{
    public static class Extensions
    {
    }
}

namespace AngleSharp.Parser.Html
{
    public class HtmlParser
    {
    }
}

namespace Ganss.XSS
{
    public class HtmlSanitizer
    {
        public string Sanitize(string html) => html;
    }
}
