using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Caching.Memory;

namespace SmartStore.Core.IO
{
    public interface IFileProvider
    {
        string MapPath(string virtualPath);
        string Combine(params string[] paths);
        string Normalize(string virtualPath);
        string ToAppRelative(string virtualPath);

        bool DirectoryExists(string virtualPath);
        bool FileExists(string virtualPath);

        CacheDependency GetCacheDependency(string virtualPath, IEnumerable<string> dependencies, DateTime utcStart);
        string GetCacheKey(string virtualPath);
        string GetFileHash(string virtualPath, IEnumerable<string> dependencies);

        IEnumerable<string> ListDirectories(string virtualPath);
        IEnumerable<string> ListFiles(string virtualPath);

        Stream OpenFile(string virtualPath);
    }

    public static class IFileProviderExtensions
    {
        public static string GetFileHash(this IFileProvider vpp, string virtualPath)
        {
            return vpp.GetFileHash(virtualPath, new[] { virtualPath });
        }

        public static CacheDependency GetCacheDependency(this IFileProvider vpp, string virtualPath, DateTime utcStart)
        {
            return vpp.GetCacheDependency(virtualPath, new[] { virtualPath }, utcStart);
        }
    }
}
