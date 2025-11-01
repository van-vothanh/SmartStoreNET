using Microsoft.Extensions.FileProviders;
﻿using SmartStore.Core.IO;

namespace SmartStore.Core
{
    public interface IApplicationEnvironment
    {
        string MachineName { get; }
        string EnvironmentIdentifier { get; }

        IFileProvider WebRootFolder { get; }
        IFileProvider AppDataFolder { get; }
        IFileProvider ThemesFolder { get; }
        IFileProvider PluginsFolder { get; }
        IFileProvider TenantFolder { get; }
    }
}
