using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

// Compatibility stubs for System.Web types that don't exist in .NET Core
// These provide minimal implementations to allow compilation

namespace System.Web
{
    public abstract class HttpContextBase
    {
        public abstract HttpRequestBase Request { get; }
        public abstract HttpResponseBase Response { get; }
        public abstract IPrincipal User { get; set; }
        public abstract IDictionary Items { get; }
    }

    public abstract class HttpRequestBase
    {
        public abstract string HttpMethod { get; }
        public abstract Uri Url { get; }
        public abstract string UserAgent { get; }
        public abstract NameValueCollection Headers { get; }
        public abstract NameValueCollection QueryString { get; }
        public abstract NameValueCollection Form { get; }
        public abstract HttpCookieCollection Cookies { get; }
        public abstract bool IsSecureConnection { get; }
        public abstract string UserHostAddress { get; }
    }

    public abstract class HttpResponseBase
    {
        public abstract int StatusCode { get; set; }
        public abstract string StatusDescription { get; set; }
        public abstract HttpCookieCollection Cookies { get; }
        public abstract NameValueCollection Headers { get; }
        public abstract TextWriter Output { get; }
    }

    public class HttpRequest : HttpRequestBase
    {
        public override string HttpMethod => throw new NotImplementedException();
        public override Uri Url => throw new NotImplementedException();
        public override string UserAgent => throw new NotImplementedException();
        public override NameValueCollection Headers => throw new NotImplementedException();
        public override NameValueCollection QueryString => throw new NotImplementedException();
        public override NameValueCollection Form => throw new NotImplementedException();
        public override HttpCookieCollection Cookies => throw new NotImplementedException();
        public override bool IsSecureConnection => throw new NotImplementedException();
        public override string UserHostAddress => throw new NotImplementedException();
    }

    public class HttpContext
    {
        public static HttpContext Current => null;
        public HttpRequest Request => throw new NotImplementedException();
        public IPrincipal User { get; set; }
    }

    public class HttpCookieCollection : NameObjectCollectionBase
    {
        public HttpCookie this[string name] => throw new NotImplementedException();
        public void Add(HttpCookie cookie) => throw new NotImplementedException();
    }

    public class HttpCookie
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime Expires { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public bool Secure { get; set; }
        public bool HttpOnly { get; set; }
    }

    public interface IHttpHandler
    {
        bool IsReusable { get; }
        void ProcessRequest(HttpContext context);
    }

    public interface IHttpModule
    {
        void Init(HttpApplication context);
        void Dispose();
    }

    public class HttpApplication
    {
        public event EventHandler BeginRequest;
        public event EventHandler EndRequest;
    }

    public abstract class HttpSessionStateBase
    {
        public abstract object this[string name] { get; set; }
        public abstract void Add(string name, object value);
        public abstract void Remove(string name);
        public abstract void Clear();
    }

    public class SessionStateItemCollection : NameObjectCollectionBase
    {
        public object this[string name] { get; set; }
    }

    public interface IRegisteredObject
    {
        void Stop(bool immediate);
    }
}

namespace System.Web.Hosting
{
    public static class HostingEnvironment
    {
        public static string ApplicationPhysicalPath => AppDomain.CurrentDomain.BaseDirectory;
        public static void RegisterObject(IRegisteredObject obj) { }
        public static void UnregisterObject(IRegisteredObject obj) { }
    }
}

namespace System.Web.Mvc
{
    public abstract class Controller
    {
        public ViewResult View() => throw new NotImplementedException();
        public ViewResult View(object model) => throw new NotImplementedException();
    }

    public class ViewResult
    {
    }

    public abstract class ActionResult
    {
    }

    public abstract class FilterAttribute : Attribute
    {
    }

    public interface IAuthorizationFilter
    {
        void OnAuthorization(AuthorizationContext filterContext);
    }

    public interface IAuthenticationFilter
    {
        void OnAuthentication(AuthenticationContext filterContext);
        void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext);
    }

    public class AuthorizationContext
    {
        public ActionResult Result { get; set; }
    }

    public class AuthenticationContext
    {
        public ActionResult Result { get; set; }
    }

    public class AuthenticationChallengeContext
    {
        public ActionResult Result { get; set; }
    }

    public class GlobalFilterCollection : List<object>
    {
    }

    public class AllowHtmlAttribute : Attribute
    {
    }

    public class ControllerContext
    {
    }

    public class RequestContext
    {
    }
}

namespace System.Web.Routing
{
    public class RouteData
    {
        public RouteValueDictionary Values { get; set; } = new RouteValueDictionary();
    }

    public class RouteValueDictionary : Dictionary<string, object>
    {
    }

    public abstract class RouteBase
    {
    }
}

namespace System.Web.Caching
{
    public class Cache
    {
        public object this[string key] { get; set; }
        public void Insert(string key, object value) { }
        public void Remove(string key) { }
    }

    public class CacheDependency
    {
    }
}

namespace System.Web.Security
{
    public interface IPrincipal
    {
        IIdentity Identity { get; }
        bool IsInRole(string role);
    }
}

namespace System.Web.UI
{
    public class HtmlTextWriter : TextWriter
    {
        public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;
    }
}

namespace System.Web.SessionState
{
    // Already defined above
}

namespace System.Web.Configuration
{
    public static class WebConfigurationManager
    {
        public static NameValueCollection AppSettings => new NameValueCollection();
    }
}

namespace System.Runtime.Caching
{
    // Already migrated to Microsoft.Extensions.Caching.Memory
}
