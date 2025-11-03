// Compatibility stub for System.Web.Hosting.IRegisteredObject
// TODO: Replace with IHostedService in ASP.NET Core

namespace System.Web.Hosting
{
    public interface IRegisteredObject
    {
        void Stop(bool immediate);
    }
    
    public static class HostingEnvironment
    {
        public static void RegisterObject(IRegisteredObject obj)
        {
            // No-op in .NET Core
        }
        
        public static void UnregisterObject(IRegisteredObject obj)
        {
            // No-op in .NET Core
        }
        
        public static string MapPath(string virtualPath)
        {
            // Simple implementation - should be replaced with IWebHostEnvironment
            return virtualPath?.Replace("~/", "").Replace("/", Path.DirectorySeparatorChar.ToString());
        }
    }
}
