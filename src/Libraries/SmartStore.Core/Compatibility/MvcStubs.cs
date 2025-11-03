// Compatibility stubs for System.Web.Mvc
// TODO: Replace with proper ASP.NET Core MVC

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace System.Web.Mvc
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
    }
    
    public class GlobalFilterCollection : List<object>
    {
    }
    
    public interface IAuthenticationFilter : IFilterMetadata
    {
        void OnAuthentication(AuthenticationContext context);
        void OnAuthenticationChallenge(AuthenticationChallengeContext context);
    }
    
    public class AuthenticationContext
    {
        public ActionContext ActionContext { get; set; }
        public object Result { get; set; }
    }
    
    public class AuthenticationChallengeContext
    {
        public ActionContext ActionContext { get; set; }
        public object Result { get; set; }
    }
}
