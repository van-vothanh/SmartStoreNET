// Compatibility stub for System.Web.HttpContextBase
// TODO: Replace with proper ASP.NET Core HttpContext usage

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace System.Web
{
    public abstract class HttpContextBase
    {
        public virtual HttpContext Context { get; set; }
        public virtual HttpRequest Request => Context?.Request;
        public virtual HttpResponse Response => Context?.Response;
        public virtual IDictionary<object, object> Items => Context?.Items;
        
        public virtual object GetService(Type serviceType)
        {
            return Context?.RequestServices?.GetService(serviceType);
        }
    }
    
    public class HttpContextWrapper : HttpContextBase
    {
        public HttpContextWrapper(HttpContext context)
        {
            Context = context;
        }
    }
}
