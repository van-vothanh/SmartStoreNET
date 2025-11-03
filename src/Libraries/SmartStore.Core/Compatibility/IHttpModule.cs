// Compatibility stub for System.Web.IHttpModule
// TODO: Replace with ASP.NET Core middleware

namespace System.Web
{
    public interface IHttpModule
    {
        void Init(HttpApplication context);
        void Dispose();
    }
    
    public class HttpApplication
    {
        public event EventHandler BeginRequest;
        public event EventHandler EndRequest;
        public event EventHandler Error;
        
        protected virtual void OnBeginRequest(EventArgs e) => BeginRequest?.Invoke(this, e);
        protected virtual void OnEndRequest(EventArgs e) => EndRequest?.Invoke(this, e);
        protected virtual void OnError(EventArgs e) => Error?.Invoke(this, e);
    }
    
    public interface IHttpHandler
    {
        void ProcessRequest(HttpContextBase context);
        bool IsReusable { get; }
    }
}
