using System.Linq;
// TODO: .NET 8 - Replace NuGet.Core with NuGet.Protocol

// TODO: .NET 8 Migration - NuGet.Core not compatible, needs replacement with NuGet.Protocol
#if FALSE_NUGET_CORE_NOT_SUPPORTED
namespace SmartStore.Core.Packaging
{
    /// <summary>
    /// This repository implementation fakes a source (remote) repository
    /// </summary>
    internal class NullSourceRepository : PackageRepositoryBase
    {
        public override IQueryable<IPackage> GetPackages()
        {
            return Enumerable.Empty<IPackage>().AsQueryable();
        }

        public override string Source => string.Empty;

        public override bool SupportsPrereleasePackages => true;

        public override void AddPackage(IPackage package) { }

        public override void RemovePackage(IPackage package) { }
    }
}
#endif
