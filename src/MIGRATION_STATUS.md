# .NET Framework to .NET 8 Migration Status

## Completed Steps

### 1. Global Search-and-Replace Operations ✅
- Removed `[AllowHtml]` attributes from all files
- Removed `[Index]` attributes from all entity classes
- Removed `[assembly: PreApplicationStartMethod]` attributes
- Replaced using statements:
  - `System.Web.Mvc` → `Microsoft.AspNetCore.Mvc`
  - `System.Web.Routing` → `Microsoft.AspNetCore.Routing`
  - `System.Web.Hosting` → `Microsoft.AspNetCore.Hosting`
  - `System.Web.Caching` → `Microsoft.Extensions.Caching.Memory`
  - `System.Web.SessionState` → `Microsoft.AspNetCore.Http`
  - `System.Web.Security` → `Microsoft.AspNetCore.Identity`
  - `System.Runtime.Caching` → `Microsoft.Extensions.Caching.Memory`
  - `System.Runtime.Remoting.Messaging` → `System.Threading`
  - `System.Data.Entity` → `Microsoft.EntityFrameworkCore`
  - `AngleSharp.Parser.Html` → `AngleSharp.Html.Parser`
  - `AngleSharp.Extensions` → `AngleSharp.Html.Dom`
  - `Ganss.XSS` → `Ganss.Xss`
- Deleted obsolete using statements:
  - `System.Data.SqlServerCe`
  - `Microsoft.Web.Infrastructure`
  - `System.Web.UI`
  - `System.Web.Compilation`
  - `System.Web.Configuration`
- Replaced type names:
  - `DbEntityEntry` → `EntityEntry`
  - `CacheItemPolicy` → `MemoryCacheEntryOptions`

### 2. Project File Conversion ✅
Converted all projects from old-style .csproj to SDK-style:
- SmartStore.Core
- SmartStore.Data
- SmartStore.Services
- SmartStore.Web.Framework
- SmartStore.Web
- SmartStore.Admin
- SmartStore.Tax
- SmartStore.Shipping
- SmartStore.DevTools
- SmartStore.OfflinePayment

### 3. Package Updates ✅
- Updated to .NET 8 compatible packages
- Fixed package version conflicts:
  - HtmlSanitizer 9.0.873
  - AngleSharp 1.1.2
  - MaxMind.GeoIP2 5.3.0 (downgraded from 6.1.0 which doesn't exist)
  - SixLabors.ImageSharp 3.1.7
  - System.Linq.Dynamic.Core 1.4.8
- Removed legacy imports from .csproj files

### 4. Target Framework ✅
All projects now target `net8.0`

## Remaining Compilation Errors

### Critical Issues Requiring Manual Migration

#### 1. System.Web Types (High Priority)
Files using these types need manual conversion:
- `HttpContextBase` → `HttpContext`
- `HttpRequestBase` → `HttpRequest`
- `HttpResponseBase` → `HttpResponse`
- `HttpSessionStateBase` → `ISession`
- `HttpCookieCollection` → `IRequestCookieCollection` / `IResponseCookies`
- `SessionStateItemCollection` → Session extensions
- `IHttpHandler` → Middleware or endpoint
- `IHttpModule` → Middleware
- `RequestContext` → `HttpContext`

**Affected Files:**
- `/Libraries/SmartStore.Core/Caching/RequestCache.cs`
- `/Libraries/SmartStore.Core/Fakes/*.cs` (entire Fakes directory)
- `/Libraries/SmartStore.Core/Events/CommonMessages/*.cs`
- `/Libraries/SmartStore.Core/WebHelper.cs`
- `/Libraries/SmartStore.Core/IWebHelper.cs`

#### 2. Entity Framework 6 → EF Core (High Priority)
- Remove `System.Data.Entity` references
- Update `DbContext` constructors to accept `DbContextOptions<T>`
- Replace `DbModelBuilder` with `ModelBuilder`
- Remove `IObjectContextAdapter` usage

**Affected Files:**
- `/Libraries/SmartStore.Core/BaseEntity.cs`
- `/Libraries/SmartStore.Core/Data/Hooks/HookedEntity.cs`
- `/Libraries/SmartStore.Core/Data/IDbContext.cs`
- `/Libraries/SmartStore.Core/PagedList`T.cs`

#### 3. NuGet.Core → NuGet.Protocol (Medium Priority)
Old NuGet v2 API needs migration to v3:
- `IPackage` → `IPackageSearchMetadata`
- `IPackageRepository` → `SourceRepository`
- `PackageRepositoryBase` → Custom implementation
- `IProjectSystem` → File system operations
- `ILogger` → `NuGet.Common.ILogger`

**Affected Files:**
- `/Libraries/SmartStore.Core/Packaging/NuGet/*.cs` (entire directory)
- `/Libraries/SmartStore.Core/Packaging/PackageBuilder.cs`
- `/Libraries/SmartStore.Core/Packaging/PackageInstaller.cs`
- `/Libraries/SmartStore.Core/Packaging/PackagingUtils.cs`

#### 4. Autofac Integration (Medium Priority)
- `Autofac.Integration.Mvc` → `Autofac.Extensions.DependencyInjection`
- `ILifetimeScopeProvider` → `IServiceScopeFactory`
- `AutofacRequestLifetimeHttpModule` → Middleware

**Affected Files:**
- `/Libraries/SmartStore.Core/Infrastructure/DependencyManagement/AutofacRequestLifetimeHttpModule.cs`
- `/Libraries/SmartStore.Core/Infrastructure/DependencyManagement/DefaultLifetimeScopeProvider.cs`
- `/Libraries/SmartStore.Core/Infrastructure/SmartStoreEngine.cs`

#### 5. Legacy Infrastructure (Medium Priority)
- `IRegisteredObject` → `IHostedService`
- `GlobalFilterCollection` → Remove (filters in Program.cs)
- `IAuthenticationFilter` → Remove (use middleware)
- `AuthenticationContext` → Remove
- `AuthenticationChallengeContext` → Remove

**Affected Files:**
- `/Libraries/SmartStore.Core/Async/AsyncRunner.cs`
- `/Libraries/SmartStore.Core/Logging/log4net/Log4netLoggerFactory.cs`
- `/Libraries/SmartStore.Core/Infrastructure/ApplicationStart.cs`
- `/Libraries/SmartStore.Core/Events/CommonMessages/AppRegisterGlobalFiltersEvent.cs`

#### 6. ASP.NET Core Identity (Low Priority)
- Add `Microsoft.AspNetCore.Identity` package
- Update authentication/authorization code

**Affected Files:**
- `/Libraries/SmartStore.Core/Extensions/HttpExtensions.cs`
- `/Libraries/SmartStore.Core/Security/SmartStorePrincipal.cs`

## Recommended Next Steps

### Phase 1: Core Infrastructure (Priority 1)
1. Comment out or stub the Fakes directory classes (test helpers)
2. Comment out NuGet packaging functionality temporarily
3. Fix Entity Framework DbContext issues
4. Fix WebHelper and IWebHelper

### Phase 2: Autofac and DI (Priority 2)
1. Update Autofac integration
2. Convert IHttpModule to middleware
3. Replace IRegisteredObject with IHostedService

### Phase 3: Authentication and Security (Priority 3)
1. Add ASP.NET Core Identity packages
2. Update authentication code
3. Update authorization filters

### Phase 4: Testing and Validation (Priority 4)
1. Restore test helper classes
2. Implement NuGet v3 API if needed
3. Full solution build verification

## Build Command
```bash
cd /data/code/src
dotnet build SmartStoreNET.Minimal.sln
```

## Current Error Count
Approximately 100+ compilation errors remaining, primarily in:
- SmartStore.Core (majority of errors)
- SmartStore.Data (EF Core migration)
- SmartStore.Services (depends on Core)

## Estimated Effort
- Phase 1: 4-6 hours
- Phase 2: 2-3 hours
- Phase 3: 2-3 hours
- Phase 4: 2-4 hours
**Total: 10-16 hours of focused development work**
