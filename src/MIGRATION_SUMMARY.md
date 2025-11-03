# SmartStoreNET .NET Framework to .NET 8 Migration Summary

## Migration Status: IN PROGRESS

This document summarizes the work completed during the initial phase of migrating SmartStoreNET from .NET Framework 4.7.2 to .NET 8.

## What Was Accomplished

### 1. Project File Modernization ✅ COMPLETE
All 10 project files were successfully converted from legacy .csproj format to SDK-style:

**Converted Projects:**
- `SmartStore.Core` → .NET 8 SDK-style
- `SmartStore.Data` → .NET 8 SDK-style  
- `SmartStore.Services` → .NET 8 SDK-style
- `SmartStore.Web.Framework` → .NET 8 SDK-style
- `SmartStore.Web` → .NET 8 SDK Web project
- `SmartStore.Admin` → .NET 8 SDK Web project
- `SmartStore.OfflinePayment` → .NET 8 SDK-style
- `SmartStore.Tax` → .NET 8 SDK-style
- `SmartStore.Shipping` → .NET 8 SDK-style
- `SmartStore.DevTools` → .NET 8 SDK-style

**Changes Made:**
- Removed `packages.config` files
- Converted to `<PackageReference>` format
- Set `<TargetFramework>net8.0</TargetFramework>`
- Removed legacy MSBuild imports (NuGet.targets, WebApplication.targets)
- Added `<FrameworkReference Include="Microsoft.AspNetCore.App" />` where needed

### 2. NuGet Package Migration ✅ COMPLETE

**Major Package Replacements:**
| Old Package | New Package | Version |
|------------|-------------|---------|
| EntityFramework 6.4.4 | Microsoft.EntityFrameworkCore | 8.0.0 |
| Autofac.Mvc5 5.0.0 | Autofac.Extensions.DependencyInjection | 9.0.0 |
| System.Web.Optimization | Removed | - |
| AngleSharp 0.9.11 | AngleSharp | 1.1.2 |
| FluentValidation 7.4.0 | FluentValidation | 11.9.2 |
| EPPlus 4.5.3 | EPPlus | 7.3.2 |
| SixLabors.ImageSharp 3.1.5 | SixLabors.ImageSharp | 3.1.6 |

**New Packages Added:**
- `Microsoft.Extensions.Caching.Memory 8.0.0`
- `System.ServiceModel.Syndication 8.0.0`
- `Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation 8.0.0`
- `Microsoft.EntityFrameworkCore.SqlServer 8.0.0`
- `Microsoft.EntityFrameworkCore.Design 8.0.0`

### 3. Pre-Build Cleanup ✅ COMPLETE

**Automated Cleanup Performed:**
- Removed all `[Index]` attributes (EF6 → EF Core incompatibility)
- Removed `[AllowHtml]` attributes (MVC5 → ASP.NET Core)
- Removed `[assembly: PreApplicationStartMethod]` attributes
- Commented out incompatible System.Web namespaces
- Commented out log4net dependencies
- Commented out NuGet.Core dependencies
- Commented out test fakes (FakeHttpContext, etc.)

### 4. Namespace Replacements ✅ COMPLETE

**Global Find/Replace Operations:**
```
System.Web.Mvc → Microsoft.AspNetCore.Mvc
System.Web.Routing → Microsoft.AspNetCore.Routing
System.Web.Hosting → Microsoft.AspNetCore.Hosting
System.Web.Caching → Microsoft.Extensions.Caching.Memory
System.Web.Http → Microsoft.AspNetCore.Mvc
System.Data.Entity → Microsoft.EntityFrameworkCore
```

### 5. Core File Migrations ✅ PARTIAL

**Successfully Migrated:**
- `BaseEntity.cs` - EF6 ObjectContext → EF Core proxy detection
- `HookedEntity.cs` - DbEntityEntry → EntityEntry (EF Core)
- `MemoryCacheManager.cs` - System.Runtime.Caching → IMemoryCache
- `IDbContext.cs` - EntityState references updated to EF Core

**Commented Out (Require Rewrite):**
- `WebHelper.cs` - Needs IHttpContextAccessor pattern
- `HttpExtensions.cs` - Needs ASP.NET Core HttpContext
- `RouteExtensions.cs` - Needs ASP.NET Core routing
- `PermissionAttribute.cs` - Needs ASP.NET Core authorization filters
- `ApplicationStart.cs` - Logic needs to move to Program.cs
- `SmartStoreEngine.cs` - Engine initialization needs redesign
- All log4net files - Replace with Microsoft.Extensions.Logging
- All NuGet.Core files - Replace with NuGet.Protocol or remove
- All test fake files - Replace with WebApplicationFactory
- Theme system files - Needs complete redesign for .NET 8
- Virtual path provider files - Replace with IFileProvider

## Current Build Status

**SmartStore.Core Project:**
- Status: Builds with warnings only (no errors when isolated)
- Warnings: 50 (mostly package version constraints)
- Commented out files: ~40 files requiring migration

**Full Solution:**
- Status: Does not build (303 errors)
- Primary issue: Dependent projects (Data, Services, Web) reference commented-out Core files
- Secondary issue: These projects have their own System.Web dependencies

## What Needs to Be Done Next

### Phase 1: Complete SmartStore.Data Migration (Est. 4-6 hours)
1. Migrate all EF6 DbContext implementations to EF Core
2. Update all entity configurations (Fluent API instead of attributes)
3. Migrate all mapping files in `Mapping/` directory
4. Update repository patterns for EF Core
5. Fix LINQ query extensions

### Phase 2: Complete SmartStore.Services Migration (Est. 6-8 hours)
1. Fix all service classes referencing commented-out Core files
2. Migrate authentication services to ASP.NET Core Identity
3. Update caching services to use IMemoryCache/IDistributedCache
4. Migrate file system services to IFileProvider
5. Update all HTTP context usages to IHttpContextAccessor

### Phase 3: Complete SmartStore.Web.Framework Migration (Est. 8-10 hours)
1. Migrate all MVC filters to ASP.NET Core filters
2. Update model binders
3. Migrate view helpers and HTML helpers
4. Update routing configuration
5. Migrate bundling/minification
6. Update dependency injection registration

### Phase 4: Complete SmartStore.Web Migration (Est. 6-8 hours)
1. Create `Program.cs` with proper startup configuration
2. Migrate `Global.asax` logic to middleware pipeline
3. Convert `Web.config` to `appsettings.json`
4. Update all controllers to ASP.NET Core MVC
5. Migrate Razor views
6. Configure static files middleware

### Phase 5: Plugin Projects Migration (Est. 4-6 hours)
1. Update plugin loading mechanism
2. Migrate plugin controllers
3. Update plugin views
4. Fix plugin dependencies

### Phase 6: Testing & Validation (Est. 6-8 hours)
1. Create integration tests
2. Test all major features
3. Performance testing
4. Fix runtime issues

## Total Estimated Time Remaining: 34-46 hours

## Files Commented Out (Require Migration)

### SmartStore.Core (40 files)
```
Async/AsyncRunner.cs
Async/LocalAsyncState.cs
Caching/RequestCache.cs
Collections/TopologicalSorter.cs
Events/CommonMessages/AppRegisterGlobalFiltersEvent.cs
Events/CommonMessages/AppStartedEvent.cs
Extensions/HttpExtensions.cs
Extensions/HtmlTextWriterExtensions.cs
Extensions/RouteExtensions.cs
Extensions/StringExtensions.cs
Fakes/*.cs (6 files)
Html/HtmlUtils.cs
Infrastructure/ApplicationEnvironment.cs
Infrastructure/ApplicationStart.cs
Infrastructure/DependencyManagement/AutofacRequestLifetimeHttpModule.cs
Infrastructure/DependencyManagement/DefaultLifetimeScopeProvider.cs
Infrastructure/IApplicationEnvironment.cs
Infrastructure/SmartStoreEngine.cs
Infrastructure/WebAppTypeFinder.cs
IO/LockFile/LockFile.cs
IO/LockFile/LockFileManager.cs
IO/VirtualPath/DefaultVirtualPathProvider.cs
IO/VirtualPath/IVirtualFolder.cs
IO/VirtualPath/IVirtualPathProvider.cs
IO/VirtualPath/VirtualFolder.cs
IWebHelper.cs
Logging/log4net/*.cs (3 files)
Packaging/*.cs (8 files)
Plugins/IConfigurable.cs
Plugins/PluginManager.cs
RouteInfo.cs
Security/PermissionAttribute.cs
Security/SmartStorePrincipal.cs
Themes/*.cs (7 files)
Utilities/FileDownloadManager.cs
WebHelper.cs
```

## Migration Patterns Applied

### 1. EF6 to EF Core
```csharp
// Before
using System.Data.Entity;
DbEntityEntry entry;
ObjectContext.GetObjectType(type);

// After
using Microsoft.EntityFrameworkCore;
EntityEntry entry;
// Proxy detection via namespace check
```

### 2. Caching
```csharp
// Before
using System.Runtime.Caching;
MemoryCache cache = new MemoryCache("name");

// After
using Microsoft.Extensions.Caching.Memory;
IMemoryCache cache; // Injected via DI
```

### 3. HTTP Context
```csharp
// Before
HttpContext.Current.Request

// After (Pattern to apply)
IHttpContextAccessor accessor; // Injected
accessor.HttpContext.Request
```

## Key Decisions Made

1. **Commented Out vs. Deleted**: Files were commented out rather than deleted to preserve business logic for reference during migration

2. **Package Versions**: Used latest stable .NET 8 packages (8.0.0) for consistency

3. **AngleSharp Version**: Kept 1.1.2 despite HtmlSanitizer wanting 0.17.1 (PreMailer.Net requires 1.1.0+)

4. **Minimal Implementation**: Created minimal implementations (e.g., MemoryCacheManager) to get builds working, with TODOs for full implementation

5. **Theme System**: Entire theme system commented out - requires complete redesign for .NET 8

6. **Plugin System**: Plugin loading mechanism needs complete rewrite for .NET 8

## Warnings to Address

- 50 package version constraint warnings (AngleSharp dependencies)
- These are non-blocking but should be resolved by updating HtmlSanitizer or using AngleSharp 0.17.1

## Next Steps for Developer

1. **Start with SmartStore.Data**: This is the foundation - get it building first
2. **Then SmartStore.Services**: Services depend on Data
3. **Then SmartStore.Web.Framework**: Framework depends on Services
4. **Finally SmartStore.Web**: Web depends on Framework
5. **Last: Plugins**: Plugins depend on everything else

## Tools & Scripts Created

- `/tmp/prebuild_cleanup.sh` - Automated pre-build cleanup
- `/tmp/comment_legacy_files.sh` - Comment out incompatible files
- `/tmp/replace_systemweb.sh` - Replace System.Web namespaces

## Documentation References

The migration followed patterns from the knowledge base:
- `00-prebuild-cleanup-and-bulk-fixes.md`
- `01-project-file-migration.md`
- `02-systemweb-migration.md`
- `06-package-migrations.md`
- `16-ef6-to-efcore-migration.md`

## Conclusion

This migration represents approximately 20-25% completion of the full .NET 8 migration. The foundation has been laid with modern project files, updated packages, and cleaned-up code. The remaining work involves systematic migration of each layer, starting from the data access layer and working up to the presentation layer.

The commented-out files serve as a roadmap for what needs to be migrated. Each file contains the original business logic that needs to be adapted to .NET 8 patterns.
