#if false // TODO: Migrate to NuGet.Protocol v3 API
﻿using System.IO;
using SmartStore.Core.Plugins;
using SmartStore.Core.Themes;

namespace SmartStore.Core.Packaging
{
    public interface IPackageBuilder
    {
        Stream BuildPackage(PluginDescriptor pluginDescriptor);
        Stream BuildPackage(ThemeManifest themeManifest);
    }
}
#endif
