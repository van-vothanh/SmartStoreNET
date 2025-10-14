using System;
using SmartStore.Core.Plugins;
using SmartStore.Core.Themes;

namespace SmartStore.Core.Packaging
{
    // TODO: Migrate to NuGet.Protocol v3 API
    public class PackageBuilder : IPackageBuilder
    {
        public PackageBuilder(IApplicationEnvironment env)
        {
        }

        public string BuildPackage(PluginDescriptor pluginDescriptor)
        {
            throw new NotImplementedException("TODO: Migrate to NuGet.Protocol v3 API");
        }

        public string BuildPackage(ThemeManifest themeManifest)
        {
            throw new NotImplementedException("TODO: Migrate to NuGet.Protocol v3 API");
        }
    }
}
