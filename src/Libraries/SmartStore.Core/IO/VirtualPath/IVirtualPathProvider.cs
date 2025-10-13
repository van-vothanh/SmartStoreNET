using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Primitives;

namespace SmartStore.Core.IO
{
    public interface IVirtualPathProvider
    {
        string MapPath(string virtualPath);
        string Combine(params string[] paths);
        string Normalize(string virtualPath);
        string ToAppRelative(string virtualPath);

        bool DirectoryExists(string virtualPath);
        bool FileExists(string virtualPath);

        // TODO: CacheDependency → IChangeToken migration
        IChangeToken GetChangeToken(string virtualPath, IEnumerable<string> dependencies);
        string GetCacheKey(string virtualPath);
        string GetFileHash(string virtualPath, IEnumerable<string> dependencies);

        IEnumerable<string> ListDirectories(string virtualPath);
        IEnumerable<string> ListFiles(string virtualPath);

        Stream OpenFile(string virtualPath);
    }

    public static class IVirtualPathProviderExtensions
    {
        public static string GetFileHash(this IVirtualPathProvider vpp, string virtualPath)
        {
            return vpp.GetFileHash(virtualPath, new[] { virtualPath });
        }

        public static IChangeToken GetChangeToken(this IVirtualPathProvider vpp, string virtualPath)
        {
            return vpp.GetChangeToken(virtualPath, new[] { virtualPath });
        }
    }
}
