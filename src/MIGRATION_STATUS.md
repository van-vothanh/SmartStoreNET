# .NET Framework to .NET 8 Migration Status

## Completed Steps

### 1. Pre-Build Cleanup ✓
- Removed [Index] EF6 attributes from all entity classes
- Removed [AllowHtml] attributes from model classes
- Removed PreApplicationStartMethod attribute from PluginManager.cs
- Replaced System.Web.* using statements with ASP.NET Core equivalents
- Removed incompatible using statements (System.Web.Compilation, System.Web.UI, etc.)

### 2. Project File Migration ✓
- Converted all .csproj files to SDK-style format
- Updated target framework from net472 to net8.0
- Removed legacy NuGet.targets imports
- Removed WebApplication.targets imports
- Added modern PackageReference elements
- Configured projects with proper dependencies

### 3. Package Updates ✓
- Updated Autofac from 5.2.0 to 8.1.0
- Updated Autofac.Mvc5 to Autofac.Extensions.DependencyInjection 10.0.0
- Updated EntityFramework 6.4.4 to Microsoft.EntityFrameworkCore 8.0.11
- Updated AngleSharp from 0.9.11 to 1.1.2
- Added System.ServiceModel.Syndication 8.0.0
- Added Microsoft.Extensions.Caching.Memory 8.0.1

## Remaining Compilation Errors (Categorized)

### Category 1: Entity Framework Migration (High Priority)
**Count**: ~50 errors
**Files Affected**: 
- BaseEntity.cs
- HookedEntity.cs
- IDbContext.cs
- PagedList`T.cs
- All Data layer files

**Required Actions**:
- Replace `System.Data.Entity` with `Microsoft.EntityFrameworkCore`
- Replace `DbEntityEntry` with `EntityEntry`
- Update DbContext base class usage
- Remove EF6-specific APIs

### Category 2: System.Web.Mvc Migration (High Priority)
**Count**: ~40 errors
**Files Affected**:
- All Controllers
- Filters
- RouteExtensions.cs
- FakeController.cs
- ApplicationStart.cs

**Required Actions**:
- Replace `System.Web.Mvc.Controller` with `Microsoft.AspNetCore.Mvc.Controller`
- Replace `GlobalFilterCollection` with filter registration in Program.cs
- Replace `IAuthenticationFilter` with ASP.NET Core authentication middleware
- Update routing to ASP.NET Core routing

### Category 3: HttpContext/HttpRequest Migration (High Priority)
**Count**: ~60 errors
**Files Affected**:
- HttpExtensions.cs
- WebHelper.cs
- RequestCache.cs
- FileDownloadManager.cs
- All Fake test classes

**Required Actions**:
- Replace `HttpContextBase` with `HttpContext`
- Replace `HttpRequestBase` with `HttpRequest`
- Replace `HttpResponseBase` with `HttpResponse`
- Update extension methods to use ASP.NET Core types
- Comment out or remove Fake test classes (not compatible with ASP.NET Core)

### Category 4: Autofac Integration Migration (Medium Priority)
**Count**: ~10 errors
**Files Affected**:
- AutofacRequestLifetimeHttpModule.cs
- DefaultLifetimeScopeProvider.cs
- SmartStoreEngine.cs

**Required Actions**:
- Replace `Autofac.Integration.Mvc` with `Autofac.Extensions.DependencyInjection`
- Replace `ILifetimeScopeProvider` with `IServiceScopeFactory`
- Convert `AutofacRequestLifetimeHttpModule` (IHttpModule) to middleware
- Update DI container registration

### Category 5: NuGet.Core Migration (Medium Priority)
**Count**: ~30 errors
**Files Affected**:
- PackageBuilder.cs
- ExtensionReferenceRepository.cs
- FileBasedProjectSystem.cs
- NugetLogger.cs
- NullSourceRepository.cs
- PackageInstaller.cs
- PackagingUtils.cs
- AppUpdater.cs

**Required Actions**:
- Replace `NuGet.Core` v2 API with `NuGet.Protocol` v3 API
- Replace `IPackage` with `IPackageSearchMetadata`
- Replace `IPackageRepository` with `SourceRepository`
- Replace `PackageRepositoryBase` with new repository pattern
- Update package installation logic

### Category 6: Caching Migration (Medium Priority)
**Count**: ~15 errors
**Files Affected**:
- MemoryCacheManager.cs
- RequestCache.cs
- HttpExtensions.cs (Cache-related methods)

**Required Actions**:
- Replace `System.Runtime.Caching.CacheItemPolicy` with `MemoryCacheEntryOptions`
- Replace `CacheItemPriority.NotRemovable` with `CacheItemPriority.NeverRemove`
- Replace `System.Web.Caching.Cache` with `IMemoryCache`
- Update cache dependency patterns

### Category 7: Infrastructure Migration (Low Priority)
**Count**: ~20 errors
**Files Affected**:
- AsyncRunner.cs (IRegisteredObject)
- Log4netLoggerFactory.cs (IRegisteredObject)
- DefaultVirtualPathProvider.cs (CacheDependency)
- IVirtualPathProvider.cs (CacheDependency)
- WebHelper.cs (AspNetHostingPermissionLevel)
- PluginManager.cs (Microsoft.Web.Infrastructure)

**Required Actions**:
- Replace `IRegisteredObject` with `IHostedService`
- Replace `CacheDependency` with `IChangeToken`
- Remove `AspNetHostingPermissionLevel` checks (always full trust in .NET 8)
- Remove `Microsoft.Web.Infrastructure.DynamicModuleHelper` usage

### Category 8: Security/Permissions Migration (Low Priority)
**Count**: ~10 errors
**Files Affected**:
- PermissionAttribute.cs
- SmartStorePrincipal.cs

**Required Actions**:
- Replace `FilterAttribute` and `IAuthorizationFilter` with ASP.NET Core filter interfaces
- Replace `AuthorizationContext` with `AuthorizationFilterContext`
- Update authentication/authorization patterns

### Category 9: HTML/UI Migration (Low Priority)
**Count**: ~5 errors
**Files Affected**:
- HtmlTextWriterExtensions.cs
- HtmlUtils.cs

**Required Actions**:
- Replace `HtmlTextWriter` with `TagBuilder` or remove
- Update `Ganss.XSS` namespace to `Ganss.Xss`

### Category 10: Configuration Migration (Low Priority)
**Count**: ~5 errors
**Files Affected**:
- IConfigurable.cs
- WebHelper.cs

**Required Actions**:
- Replace `RouteValueDictionary` usage
- Update configuration access patterns

## Next Steps (Priority Order)

1. **Fix Entity Framework errors** - This blocks Data layer compilation
2. **Fix System.Web.Mvc errors** - This blocks Controllers and Filters
3. **Fix HttpContext errors** - This blocks most web-related functionality
4. **Fix Autofac errors** - This blocks DI container setup
5. **Fix NuGet.Core errors** - This blocks plugin/package management
6. **Fix remaining infrastructure errors** - Lower priority features

## Estimated Remaining Work

- **High Priority Errors**: ~150 errors
- **Medium Priority Errors**: ~55 errors
- **Low Priority Errors**: ~40 errors
- **Total Remaining**: ~245 compilation errors

## Build Command

```bash
cd /data/code/src
dotnet build SmartStoreNET.Minimal.sln
```

## Notes

- All projects now target .NET 8
- All projects use SDK-style format
- Package versions are updated to .NET 8 compatible versions
- Some features may need to be temporarily disabled or stubbed out
- Test projects (Fake classes) may need significant rework or removal
- Plugin system may need architectural changes for .NET 8 compatibility
