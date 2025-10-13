using System;
using System.Collections.Generic;
using System.Linq;
// TODO: Migrate to NuGet.Protocol v3 API
// This class used NuGet.Core v2 API which is obsolete
// See: https://docs.microsoft.com/en-us/nuget/reference/nuget-client-sdk
using SmartStore.Core.Plugins;
using SmartStore.Core.Themes;

namespace SmartStore.Core.Packaging
{
    // TODO: Migrate to NuGet.Protocol v3 API
    // Original implementation commented out - uses obsolete NuGet.Core
    /*
    internal abstract class ExtensionReferenceRepository : PackageRepositoryBase
    {
        public ExtensionReferenceRepository(IProjectSystem project, IPackageRepository sourceRepository)
        {
            Guard.NotNull(project, nameof(project));
            Guard.NotNull(sourceRepository, nameof(sourceRepository));
            Project = project;
            SourceRepository = sourceRepository;
        }
        public IProjectSystem Project { get; set; }
        public IPackageRepository SourceRepository { get; set; }
        public override void AddPackage(IPackage package) { }
        public override void RemovePackage(IPackage package) { }
        public override string Source => Project.Root;
        public override bool SupportsPrereleasePackages => true;
    }
    */

    // Temporary stub to allow compilation
    internal abstract class ExtensionReferenceRepository
    {
        protected ExtensionReferenceRepository()
        {
            throw new NotImplementedException("TODO: Migrate to NuGet.Protocol v3 API");
        }
    }

    /// <summary>
    /// This repository implementation informs about what plugin packages are already installed.
    /// </summary>
    internal class PluginReferenceRepository : ExtensionReferenceRepository
    {
        public PluginReferenceRepository(IPluginFinder pluginFinder)
        {
            throw new NotImplementedException("TODO: Migrate to NuGet.Protocol v3 API");
        }
    }

    /// <summary>
    /// This repository implementation informs about what theme packages are already installed.
    /// </summary>
    internal class ThemeReferenceRepository : ExtensionReferenceRepository
    {
        public ThemeReferenceRepository(IThemeRegistry themeRegistry)
        {
            throw new NotImplementedException("TODO: Migrate to NuGet.Protocol v3 API");
        }
    }
}
