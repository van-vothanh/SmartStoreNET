using System;
using System.Collections.Generic;
using SmartStore.Core.IO;

namespace SmartStore.Core.IO.VirtualPath
{
    public interface IVirtualPathProvider
    {
        string GetFileHash(string virtualPath);
        string GetFileHash(string virtualPath, IEnumerable<string> dependencies);
        
        // TODO: Replace CacheDependency with IChangeToken
        // CacheDependency GetCacheDependency(string virtualPath, IEnumerable<string> dependencies, DateTime utcStart);
    }

    public static class IVirtualPathProviderExtensions
    {
        public static string GetFileHash(this IVirtualPathProvider vpp, string virtualPath)
        {
            return vpp.GetFileHash(virtualPath, new[] { virtualPath });
        }

        // TODO: Replace CacheDependency with IChangeToken
        /*
        public static CacheDependency GetCacheDependency(this IVirtualPathProvider vpp, string virtualPath, DateTime utcStart)
        {
            return vpp.GetCacheDependency(virtualPath, new[] { virtualPath }, utcStart);
        }
        */
    }
}
