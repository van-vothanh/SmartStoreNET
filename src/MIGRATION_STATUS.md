# .NET Framework to .NET 8 Migration Status

## Current Status
- **Build Errors**: 31 (down from 690+ initially)
- **Target Framework**: .NET 8.0
- **Project Files**: All converted to SDK-style format

## Completed Work

### 1. Project File Conversion ✅
- Converted all `.csproj` files from legacy format to SDK-style
- Updated target framework to `net8.0`
- Migrated from `packages.config` to `PackageReference`
- Removed legacy MSBuild imports

### 2. Package Updates ✅
- Updated Entity Framework 6 references to Entity Framework Core 8.0.1
- Updated Autofac to version 8.0.0 with Extensions.DependencyInjection
- Replaced System.Web.Mvc with Microsoft.AspNetCore.Mvc
- Updated AngleSharp, HtmlSanitizer, and other dependencies
- Fixed package version conflicts

### 3. Namespace Migrations ✅
- Replaced `System.Web.*` with `Microsoft.AspNetCore.*` equivalents
- Replaced `System.Data.Entity` with `Microsoft.EntityFrameworkCore`
- Updated `System.Runtime.Caching` to `Microsoft.Extensions.Caching.Memory`
- Fixed Autofac.Integration namespaces

### 4. Code Transformations ✅
- Removed EF6 `[Index]` attributes (will be configured in OnModelCreating)
- Removed `[AllowHtml]` attributes (not needed in ASP.NET Core)
- Replaced `DbEntityEntry` with `EntityEntry`
- Updated `IHtmlString` to `IHtmlContent`
- Fixed `LocalizedString` to implement `IHtmlContent`

### 5. Incompatible Code Commented Out ✅
The following files/classes have been commented out with `#if FALSE` directives and TODO markers:

#### NuGet.Core Dependencies
- `Packaging/NuGet/*` - Requires replacement with NuGet.Protocol
- `PackageBuilder.cs`
- `PackageInstaller.cs`
- `PackagingUtils.cs`
- `Updater/AppUpdater.cs`

#### System.Web Dependencies
- `Fakes/*` - Test utilities requiring complete rewrite
- `IO/VirtualPath/DefaultVirtualPathProvider.cs` - Replace with IFileProvider
- `IO/VirtualPath/IVirtualPathProvider.cs` - Replace with IFileProvider
- `Infrastructure/ApplicationStart.cs` - Move to Program.cs
- `Infrastructure/DependencyManagement/AutofacRequestLifetimeHttpModule.cs` - Replace with middleware
- `Infrastructure/DependencyManagement/DefaultLifetimeScopeProvider.cs` - Use built-in DI
- `Extensions/HtmlTextWriterExtensions.cs` - HtmlTextWriter not supported
- `Events/CommonMessages/AppRegisterGlobalFiltersEvent.cs` - GlobalFilterCollection not supported

#### Requires Significant Refactoring
- `WebHelper.cs` - Extensive System.Web dependencies
- `Security/PermissionAttribute.cs` - Filter architecture changed
- `Infrastructure/ApplicationEnvironment.cs` - Replace with IWebHostEnvironment
- `Infrastructure/IApplicationEnvironment.cs` - Replace with IWebHostEnvironment

#### IRegisteredObject (Hosting)
- `Async/AsyncRunner.cs` - BackgroundWorkHost class (replace with IHostedService)
- `Logging/log4net/Log4netLoggerFactory.cs` - IRegisteredObject implementation

## Remaining Work

### Critical (31 Errors Remaining)

1. **Index Attributes** (10 errors)
   - Some `[Index]` attributes still remain in domain entities
   - Need to remove and configure in EF Core OnModelCreating

2. **HttpExtensions.cs** (8 errors)
   - Duplicate method definitions
   - Cache type references
   - Fakes namespace reference

3. **IApplicationEnvironment** (4 errors)
   - `DefaultThemeRegistry.cs`
   - `LockFileManager.cs`
   - Replace with `IWebHostEnvironment`

4. **IVirtualFolder** (2 errors)
   - `LockFile.cs`
   - Replace with `IFileProvider`

5. **SyndicationFeed** (3 errors)
   - `SmartSyndicationFeed.cs`
   - Package reference exists but types not resolving

6. **Other** (4 errors)
   - `MemoryCacheManager.cs` - CacheItemPolicy
   - `PackageBuilder.cs` - NuGet reference
   - `MenuRecord.cs` - Index attribute

### Medium Priority

1. **Data Layer Migration**
   - Complete EF6 to EF Core migration in SmartStore.Data
   - Update all DbContext implementations
   - Migrate all entity configurations to EF Core fluent API
   - Update migrations to EF Core format

2. **Services Layer**
   - Fix all System.Web dependencies in SmartStore.Services
   - Update authentication/authorization services
   - Migrate caching implementations

3. **Web Layer**
   - Complete MVC controller migration
   - Update Razor views
   - Migrate bundling/minification to WebOptimizer
   - Update routing configuration
   - Create proper Program.cs with full configuration

4. **Plugin System**
   - Update plugin loading mechanism
   - Remove PreApplicationStartMethod usage
   - Implement new plugin discovery

### Low Priority

1. **Test Projects**
   - Update test frameworks
   - Fix test utilities (Fakes)
   - Update mocking strategies

2. **Documentation**
   - Update deployment guides
   - Document breaking changes
   - Create migration guide for custom plugins

## Next Steps

1. Remove remaining `[Index]` attributes
2. Fix HttpExtensions.cs duplicate methods
3. Replace IApplicationEnvironment with IWebHostEnvironment
4. Replace IVirtualFolder with IFileProvider
5. Fix SyndicationFeed package reference
6. Complete Data layer EF Core migration
7. Implement Program.cs with full startup configuration
8. Test and fix runtime issues

## Migration Patterns Applied

- **System.Web.HttpContext** → **Microsoft.AspNetCore.Http.HttpContext**
- **System.Web.Mvc.Controller** → **Microsoft.AspNetCore.Mvc.Controller**
- **System.Data.Entity.DbContext** → **Microsoft.EntityFrameworkCore.DbContext**
- **System.Web.Hosting.HostingEnvironment** → **Microsoft.AspNetCore.Hosting.IWebHostEnvironment**
- **System.Web.Caching.Cache** → **Microsoft.Extensions.Caching.Memory.IMemoryCache**
- **System.Web.Mvc.FilterAttribute** → **Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute**

## Notes

- This is a PRODUCTION migration requiring completion
- All commented-out code is marked with TODO comments
- Package versions are set to .NET 8.0.1 for consistency
- Some features may require architectural changes (e.g., plugin system)
- Testing will be required after compilation succeeds

## Build Command

```bash
cd /data/code/src
dotnet build SmartStoreNET.Minimal.sln
```

Current result: 31 errors (down from 690+)
