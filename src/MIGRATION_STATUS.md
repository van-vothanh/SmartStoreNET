# .NET Framework to .NET 8 Migration Status

## Date: 2025-11-01

## Completed Steps

### 1. Project File Conversion ✅
- Converted all `.csproj` files from old-style to SDK-style format
- Set `TargetFramework` to `net8.0`
- Migrated from `packages.config` to `PackageReference` format
- Projects converted:
  - SmartStore.Core
  - SmartStore.Data
  - SmartStore.Services
  - SmartStore.Web.Framework
  - SmartStore.Web
  - SmartStore.Admin
  - SmartStore.OfflinePayment
  - SmartStore.Tax
  - SmartStore.Shipping
  - SmartStore.DevTools

### 2. Package Migration ✅
- Updated Entity Framework 6 → Entity Framework Core 8.0.1
- Updated Autofac 5.2.0 → 8.0.0
- Added Autofac.Extensions.DependencyInjection 9.0.0
- Updated AngleSharp to 1.1.0 (for PreMailer.Net compatibility)
- Replaced ImageProcessor → SixLabors.ImageSharp 3.1.6
- Replaced LumenWorksCsvReader → CsvHelper 33.0.1
- Replaced ncrontab → Cronos 0.8.4
- Replaced NReco.PdfGenerator → DinkToPdf 1.0.8
- Removed WebOptimizer packages (not available)
- Removed NuGet.Core (obsolete)
- Added Microsoft.Extensions.Caching.Memory
- Added Microsoft.AspNetCore.Http.Abstractions
- Added Microsoft.AspNetCore.Routing

### 3. Code Cleanup ✅
- Removed all `[Index]` attributes (EF6-specific)
- Removed all `[AllowHtml]` attributes
- Removed `[assembly: PreApplicationStartMethod]` attributes
- Disabled `GenerateAssemblyInfo` to avoid duplicate assembly attributes

### 4. Using Statement Replacements ✅ (Partial)
- `using System.Web.Mvc` → `using Microsoft.AspNetCore.Mvc`
- `using System.Web.Http` → `using Microsoft.AspNetCore.Mvc`
- `using System.Web.Routing` → `using Microsoft.AspNetCore.Routing`
- `using System.Web.Security` → `using Microsoft.AspNetCore.Identity`
- `using System.Web.Caching` → `using Microsoft.Extensions.Caching.Memory`
- `using System.Web.Configuration` → `using Microsoft.Extensions.Configuration`
- `using System.Web.SessionState` → `using Microsoft.AspNetCore.Http`
- `using System.Data.Entity` → `using Microsoft.EntityFrameworkCore`
- `using System.Runtime.Caching` → `using Microsoft.Extensions.Caching.Memory`

## Remaining Work

### Critical Issues (Must Fix for Compilation)

#### 1. System.Web Dependencies (~200 errors)
Files still referencing System.Web types that need migration:
- `System.Web.Hosting.IRegisteredObject` → Need to replace with `IHostedService`
- `System.Web.Hosting.HostingEnvironment` → `IWebHostEnvironment`
- `System.Web.Hosting.VirtualPathProvider` → `IFileProvider`
- `System.Web.HttpContext` → `HttpContext` (Microsoft.AspNetCore.Http)
- `System.Web.HttpContextBase` → `HttpContext`
- `System.Web.HttpRequest` → `HttpRequest` (Microsoft.AspNetCore.Http)
- `System.Web.Compilation.BuildManager` → Remove or replace with assembly loading
- `System.Web.UI.HtmlTextWriter` → Custom implementation or remove
- `System.Web.Mvc.GlobalFilterCollection` → Use `MvcOptions.Filters`

#### 2. Entity Framework 6 to EF Core Migration (~50 errors)
- Replace `DbSet<T>.SqlQuery` → `DbSet<T>.FromSqlRaw`
- Replace `Database.ExecuteSqlCommand` → `Database.ExecuteSqlRaw`
- Remove `DbModelBuilder` → Use `ModelBuilder` in `OnModelCreating`
- Replace `DbEntityEntry` → `EntityEntry`
- Update all entity configurations to use Fluent API

#### 3. Autofac Integration (~30 errors)
- `Autofac.Integration.Mvc` → `Autofac.Extensions.DependencyInjection`
- Replace `AutofacDependencyResolver` → Use `IServiceProvider`
- Update lifetime scopes for ASP.NET Core

#### 4. NuGet.Core References (~20 errors)
- Remove or replace NuGet.Core package management
- Consider using NuGet.Protocol for package operations

#### 5. Caching Migration (~15 errors)
- `System.Runtime.Caching.MemoryCache` → `Microsoft.Extensions.Caching.Memory.IMemoryCache`
- `CacheItemPolicy` → `MemoryCacheEntryOptions`
- Update all cache implementations

#### 6. Routing Migration (~10 errors)
- `System.Web.Routing.RouteValueDictionary` → `Microsoft.AspNetCore.Routing.RouteValueDictionary`
- Update route configuration to ASP.NET Core style

### Files Requiring Major Refactoring

#### SmartStore.Core
- `/Async/AsyncRunner.cs` - IRegisteredObject usage
- `/Async/LocalAsyncState.cs` - MemoryCache usage
- `/Caching/MemoryCacheManager.cs` - Complete rewrite for IMemoryCache
- `/Caching/RequestCache.cs` - HttpContextBase → HttpContext
- `/Infrastructure/ContextState.cs` - Remoting.Messaging (obsolete)
- `/Infrastructure/DependencyManagement/AutofacRequestLifetimeHttpModule.cs` - HTTP module → middleware
- `/Infrastructure/DependencyManagement/DefaultLifetimeScopeProvider.cs` - Autofac.Integration.Mvc
- `/Infrastructure/SmartStoreEngine.cs` - Complete DI rewrite
- `/Infrastructure/WebAppTypeFinder.cs` - HostingEnvironment usage
- `/IO/LocalFileSystem.cs` - VirtualPathProvider → IFileProvider
- `/IO/VirtualPath/DefaultVirtualPathProvider.cs` - Complete rewrite
- `/Packaging/**` - NuGet.Core removal
- `/Plugins/PluginManager.cs` - BuildManager, dynamic module loading
- `/WebHelper.cs` - HttpContext.Current → IHttpContextAccessor
- `/Fakes/**` - Test fakes need ASP.NET Core equivalents

#### SmartStore.Data
- All migration files need EF Core syntax
- `ObjectContextBase.cs` - DbContext API changes
- `EfRepository.cs` - EF Core repository pattern

#### SmartStore.Services
- Hundreds of files with HttpContext dependencies
- Service layer needs IHttpContextAccessor injection

#### SmartStore.Web.Framework
- Complete MVC framework migration
- Filters, model binders, view engines
- Bundling and minification replacement

#### SmartStore.Web
- Global.asax → Program.cs
- Web.config → appsettings.json
- Startup configuration

## Migration Strategy

### Phase 1: Core Infrastructure (Current)
1. ✅ Convert project files
2. ✅ Update packages
3. ⏳ Fix System.Web references in SmartStore.Core
4. ⏳ Migrate caching infrastructure
5. ⏳ Update DI container integration

### Phase 2: Data Layer
1. Migrate EF6 DbContext to EF Core
2. Update all entity configurations
3. Migrate database migrations
4. Update repository pattern

### Phase 3: Service Layer
1. Remove HttpContext.Current dependencies
2. Inject IHttpContextAccessor where needed
3. Update service registrations

### Phase 4: Web Layer
1. Create Program.cs
2. Migrate Global.asax logic
3. Update controllers and filters
4. Migrate views and Razor syntax
5. Replace bundling/minification

### Phase 5: Testing & Validation
1. Fix compilation errors
2. Run unit tests
3. Integration testing
4. Performance validation

## Current Error Count: ~360 compilation errors

## Estimated Remaining Effort
- Core Infrastructure: 40-60 hours
- Data Layer: 20-30 hours
- Service Layer: 30-40 hours
- Web Layer: 40-50 hours
- Testing: 20-30 hours

**Total: 150-210 hours of development work**

## Recommendations

1. **Incremental Approach**: Focus on getting SmartStore.Core to compile first
2. **Automated Refactoring**: Use Roslyn analyzers and code fixes where possible
3. **Test Coverage**: Ensure comprehensive tests before migration
4. **Parallel Development**: Consider maintaining .NET Framework version during migration
5. **External Dependencies**: Evaluate if all plugins are necessary for initial migration

## Next Immediate Steps

1. Comment out or stub problematic code in SmartStore.Core to achieve compilation
2. Create ASP.NET Core equivalents for critical infrastructure (caching, DI, file providers)
3. Migrate one controller end-to-end as a proof of concept
4. Document patterns for team to follow

## Notes

- This is a PRODUCTION MIGRATION requiring systematic completion
- Cannot run application until ALL compilation errors are resolved
- Some features may need architectural changes (e.g., plugin system)
- Consider creating a migration branch and incremental PRs
