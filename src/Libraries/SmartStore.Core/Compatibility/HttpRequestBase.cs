// Compatibility stub for System.Web.HttpRequestBase
// TODO: Replace with proper ASP.NET Core HttpRequest usage

using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace System.Web
{
    public abstract class HttpRequestBase
    {
        public virtual HttpRequest Request { get; set; }
        public virtual string UserAgent => Request?.Headers["User-Agent"].ToString();
        public virtual Uri Url => new Uri(Request?.GetDisplayUrl() ?? "http://localhost");
        public virtual string RawUrl => Request?.Path + Request?.QueryString;
        public virtual NameValueCollection Headers => new NameValueCollection();
        public virtual NameValueCollection QueryString => new NameValueCollection();
        public virtual NameValueCollection Form => new NameValueCollection();
        public virtual bool IsSecureConnection => Request?.IsHttps ?? false;
        public virtual string HttpMethod => Request?.Method;
    }
    
    public class HttpRequestWrapper : HttpRequestBase
    {
        public HttpRequestWrapper(HttpRequest request)
        {
            Request = request;
        }
    }
}
