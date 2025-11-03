#if false // Disabled for .NET 8 - NuGet.Core is obsolete
﻿using System.IO;

namespace SmartStore.Core.Packaging
{
    public interface IPackageManager
    {
        PackageInfo Install(Stream packageStream, string location, string applicationPath);
        void Uninstall(string packageId, string applicationPath);

        PackagingResult BuildPluginPackage(string pluginName);
        PackagingResult BuildThemePackage(string themeName);
    }
}
#endif
