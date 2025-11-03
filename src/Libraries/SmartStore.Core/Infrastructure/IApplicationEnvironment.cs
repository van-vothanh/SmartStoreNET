#if false // Disabled for .NET 8 - uses IVirtualFolder/IVirtualPathProvider
﻿using SmartStore.Core.IO;

namespace SmartStore.Core
{
    public interface IApplicationEnvironment
    {
        string MachineName { get; }
        string EnvironmentIdentifier { get; }

        IVirtualFolder WebRootFolder { get; }
        IVirtualFolder AppDataFolder { get; }
        IVirtualFolder ThemesFolder { get; }
        IVirtualFolder PluginsFolder { get; }
        IVirtualFolder TenantFolder { get; }
    }
}
#endif
