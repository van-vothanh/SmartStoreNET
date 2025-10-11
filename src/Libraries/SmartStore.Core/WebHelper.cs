using System;
using SmartStore.Core;
// TODO: Migrate to ASP.NET Core
// This file uses System.Web.HttpContext and related types

namespace SmartStore.Core
{
    public partial class WebHelper : IWebHelper
    {
        public WebHelper()
        {
            // Stub implementation
        }
        
        public string GetUrlReferrer() => string.Empty;
        public string GetClientIdent() => string.Empty;
        public string GetCurrentIpAddress() => string.Empty;
        public string GetThisPageUrl(bool includeQueryString) => string.Empty;
        public string GetThisPageUrl(bool includeQueryString, bool useSsl) => string.Empty;
        public bool IsCurrentConnectionSecured() => false;
        public string ServerVariables(string name) => string.Empty;
        public string GetStoreHost(bool useSsl) => string.Empty;
        public string GetStoreLocation() => string.Empty;
        public string GetStoreLocation(bool useSsl) => string.Empty;
        public bool IsStaticResource(Microsoft.AspNetCore.Http.HttpRequest request) => false;
        public string MapPath(string path) => path;
        public string ModifyQueryString(string url, string queryStringModification, string anchor) => url;
        public string RemoveQueryString(string url, string queryString) => url;
        public T QueryString<T>(string name) => default(T);
        public void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "") { }
        public void RestartAppDomain(bool makeRedirect, string redirectUrl, bool aggressive) { }
        public bool IsRequestBeingRedirected => false;
        public bool IsPostBeingDone => false;
    }
}
