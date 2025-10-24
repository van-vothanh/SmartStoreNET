// TODO: .NET 8 Migration - File requires significant refactoring
#if FALSE_REQUIRES_REFACTORING
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
