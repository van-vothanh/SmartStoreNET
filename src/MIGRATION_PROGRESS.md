# SmartStoreNET .NET Framework to .NET 8 Migration Progress

## Completed Steps

### 1. Project File Conversion ✅
- Converted all `.csproj` files to SDK-style format
- Updated target framework to `net8.0`
- Replaced `packages.config` with `PackageReference`
- Updated NuGet packages to .NET 8 compatible versions

**Projects Converted:**
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

### 2. Package Migrations ✅
**Replaced:**
- `EntityFramework 6.4.4` → `Microsoft.EntityFrameworkCore 8.0.0`
- `Autofac.Mvc5` → `Autofac.Extensions.DependencyInjection 9.0.0`
- `System.Web.Optimization` → Removed (will use built-in bundling)
- `AngleSharp 0.9.11` → `AngleSharp 1.1.2`
- `FluentValidation 7.4.0` → `FluentValidation 11.9.2`
- `EPPlus 4.5.3` → `EPPlus 7.3.2`
- `SixLabors.ImageSharp 3.1.5` → `SixLabors.ImageSharp 3.1.6`

**Added:**
- `Microsoft.Extensions.Caching.Memory 8.0.0`
- `System.ServiceModel.Syndication 8.0.0`
- `Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation 8.0.0`

### 3. Pre-Build Cleanup ✅
- Removed all `[Index]` attributes (EF6 → EF Core migration)
- Removed `[AllowHtml]` attributes
- Removed `[assembly: PreApplicationStartMethod]` attributes
- Commented out incompatible System.Web namespaces
- Commented out log4net dependencies
- Commented out NuGet.Core dependencies
- Commented out test fakes (FakeHttpContext, etc.)

### 4. Namespace Replacements ✅
**Replaced:**
- `System.Web.Mvc` → `Microsoft.AspNetCore.Mvc`
- `System.Web.Routing` → `Microsoft.AspNetCore.Routing`
- `System.Web.Hosting` → `Microsoft.AspNetCore.Hosting`
- `System.Web.Caching` → `Microsoft.Extensions.Caching.Memory`
- `System.Web.Http` → `Microsoft.AspNetCore.Mvc`
- `System.Data.Entity` → `Microsoft.EntityFrameworkCore`

### 5. Core File Fixes ✅
- **BaseEntity.cs**: Migrated from EF6 ObjectContext to EF Core proxy detection
- **HookedEntity.cs**: Migrated from DbEntityEntry to EntityEntry
- **MemoryCacheManager.cs**: Replaced System.Runtime.Caching.MemoryCache with IMemoryCache

## Remaining Work

### Critical (Required for Build)

#### 1. System.Web Dependencies (~50 files)
Files still referencing System.Web types that need manual migration:
- `AsyncRunner.cs` - IRegisteredObject → IHostedService
- `WebHelper.cs` - HttpContext.Current → IHttpContextAccessor
- `RouteInfo.cs` - RouteValueDictionary migration
- `HttpExtensions.cs` - HttpContext extensions
- `ApplicationStart.cs` - Global.asax logic → Program.cs
- `SmartStoreEngine.cs` - Engine initialization
- `WebAppTypeFinder.cs` - Assembly discovery
- `DirectoryHasher.cs`, `LocalFileSystem.cs` - VirtualPathProvider → IFileProvider
- `DefaultVirtualPathProvider.cs`, `IVirtualPathProvider.cs` - File system abstraction
- `PermissionAttribute.cs` - MVC filter migration
- `CommonHelper.cs` - Utility methods
- `IWebHelper.cs` - Interface updates

#### 2. EF Core Migration (~30 files)
- `IDbContext.cs` - DbSet<> and context interface
- `IDbContextExtensions.cs` - Extension methods
- `IQueryableExtensions.cs` - LINQ extensions
- `RepositoryExtensions.cs` - Repository patterns
- `RuleStorage.cs` - Data access
- All mapping files in `SmartStore.Data/Mapping/` directory

#### 3. MVC/WebAPI Migration (~40 files)
- Controller base classes
- Action filters
- Model binders
- View helpers
- Routing configuration

#### 4. Authentication & Security (~15 files)
- `SmartStorePrincipal.cs` - Claims-based identity
- Permission system
- Authentication middleware

#### 5. Dependency Injection (~10 files)
- `AutofacRequestLifetimeHttpModule.cs` - Middleware conversion
- `DefaultLifetimeScopeProvider.cs` - Scope management
- `DependencyRegistrar.cs` files - Service registration

### Medium Priority

#### 6. Configuration Migration
- `Web.config` → `appsettings.json`
- `Global.asax` → `Program.cs`
- Connection strings
- App settings

#### 7. Static Files & Bundling
- CSS/JS bundling strategy
- Static file middleware
- CDN configuration

#### 8. Logging Migration
- log4net → Microsoft.Extensions.Logging
- Log configuration
- Custom log providers

### Low Priority

#### 9. Testing Infrastructure
- Test helpers migration
- Mock objects
- Integration tests

#### 10. Plugin System
- Plugin loading mechanism
- Plugin configuration
- Plugin dependencies

## Build Status

**Current Errors:** ~150-200 compilation errors
**Error Types:**
- Missing System.Web types: ~80 errors
- Missing EF6 types: ~40 errors
- Missing MVC5 types: ~30 errors
- Other: ~40 errors

## Next Steps

1. **Fix IDbContext and EF Core integration** (highest priority)
   - Update IDbContext interface
   - Migrate DbSet<> usage
   - Fix entity configurations

2. **Complete System.Web migration**
   - Implement IHttpContextAccessor pattern
   - Migrate HttpContext.Current usages
   - Update routing

3. **Create Program.cs**
   - Configure services
   - Configure middleware pipeline
   - Set up dependency injection

4. **Fix remaining compilation errors**
   - Work through errors systematically
   - Test each fix
   - Document breaking changes

## Estimated Time to Completion

- **Critical fixes:** 8-12 hours
- **Medium priority:** 4-6 hours
- **Low priority:** 2-4 hours
- **Testing & validation:** 4-6 hours

**Total:** 18-28 hours of focused development work

## Migration Strategy

1. Focus on getting SmartStore.Core to compile first
2. Then SmartStore.Data
3. Then SmartStore.Services
4. Finally the web projects

This bottom-up approach ensures dependencies are resolved in order.

## Notes

- Many legacy features (NuGet packaging, log4net, test fakes) have been commented out
- These can be re-implemented using .NET 8 equivalents after core functionality works
- The plugin system will need significant rework for .NET 8
- Consider using minimal APIs for some endpoints instead of full MVC controllers
