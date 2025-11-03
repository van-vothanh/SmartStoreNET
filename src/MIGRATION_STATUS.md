# SmartStoreNET .NET Framework to .NET 8 Migration Status

## Migration Progress

### Completed Tasks

1. **Project File Conversion** ✅
   - Converted all .csproj files from old-style to SDK-style format
   - Updated all projects to target `net8.0`
   - Removed old NuGet.targets references
   - Removed Microsoft.WebApplication.targets references

2. **Package Updates** ✅
   - Replaced System.Data.Entity with Microsoft.EntityFrameworkCore 8.0.0
   - Replaced System.Runtime.Caching with Microsoft.Extensions.Caching.Memory 8.0.1
   - Added ASP.NET Core framework references
   - Updated Autofac to version 8.0.0 with .NET Core integration
   - Added required packages: AngleSharp, HtmlSanitizer, log4net, etc.

3. **Namespace Migrations** ✅ (Partial)
   - Replaced `System.Data.Entity` → `Microsoft.EntityFrameworkCore`
   - Replaced `System.Runtime.Caching` → `Microsoft.Extensions.Caching.Memory`
   - Commented out System.Web.* namespaces for future migration
   - Created compatibility stubs for System.Web types

4. **Code Fixes** ✅ (Partial)
   - Removed PreApplicationStartMethod attribute (not supported in .NET Core)
   - Fixed BaseEntity to work without EF6's ObjectContext
   - Updated LocalAsyncState to use IMemoryCache instead of MemoryCache
   - Fixed AsyncRunner to remove System.Web.Hosting dependencies
   - Commented out EF6 Index attributes (need Fluent API migration)

5. **Compatibility Layer** ✅
   - Created compatibility stubs for:
     - HttpContextBase
     - HttpRequestBase
     - IHttpModule / HttpApplication
     - System.Web.Mvc types
     - Cache
     - HtmlTextWriter
     - SessionState
     - IRegisteredObject
     - Autofac.Integration.Mvc types

### Current Status

**Build Errors: 152** (down from 654+)

### Remaining Work

#### High Priority

1. **System.Web.Mvc Migration**
   - Replace all Controller base classes with ASP.NET Core MVC controllers
   - Migrate ActionResult types
   - Update filter attributes (AuthorizationContext, etc.)
   - Migrate routing from System.Web.Routing to Microsoft.AspNetCore.Routing

2. **Entity Framework Migration**
   - Migrate all EF6 Index attributes to EF Core Fluent API
   - Update DbContext implementations
   - Migrate data annotations that changed between EF6 and EF Core
   - Update LINQ queries that use EF6-specific methods

3. **HTTP Context Migration**
   - Replace HttpContextBase with HttpContext
   - Replace HttpRequestBase with HttpRequest
   - Update all HTTP-related code to use ASP.NET Core abstractions

4. **Dependency Injection**
   - Migrate Autofac.Integration.Mvc to Autofac.Extensions.DependencyInjection
   - Update lifetime scope management for ASP.NET Core
   - Remove IHttpModule implementations, replace with middleware

5. **Configuration Migration**
   - Migrate Web.config to appsettings.json
   - Update configuration access patterns
   - Migrate connection strings

6. **View Engine Migration**
   - Update Razor views to ASP.NET Core Razor syntax
   - Migrate HTML helpers
   - Update bundling and minification

#### Medium Priority

1. **Caching**
   - Complete migration from System.Runtime.Caching to IMemoryCache
   - Update CacheItemPolicy usage to MemoryCacheEntryOptions

2. **Session State**
   - Migrate from System.Web.SessionState to ASP.NET Core Session
   - Update session access patterns

3. **Authentication & Authorization**
   - Migrate from System.Web.Security to ASP.NET Core Identity
   - Update authentication filters
   - Migrate authorization attributes

4. **Static Files & Assets**
   - Update static file serving
   - Migrate bundling and minification to WebOptimizer or similar

#### Low Priority

1. **Logging**
   - Consider migrating from log4net to Microsoft.Extensions.Logging
   - Update logging configuration

2. **Testing**
   - Update test projects to work with .NET 8
   - Fix test dependencies

3. **Performance Optimization**
   - Review and optimize for .NET 8 performance characteristics
   - Update async/await patterns where beneficial

### Error Categories

1. **Type Not Found (CS0246)**: 184 errors
   - Mostly System.Web.Mvc types
   - Some EF6 types
   - Missing ASP.NET Core equivalents

2. **Invalid Attribute (CS0616)**: 72 errors
   - EF6 Index attributes need migration to Fluent API

3. **No Suitable Method to Override (CS0115)**: 32 errors
   - HttpContextBase/HttpRequestBase compatibility issues
   - Need proper ASP.NET Core implementations

4. **Type Mismatch (CS1715)**: 4 errors
   - Return type mismatches in overridden methods

### Next Steps

1. Complete the System.Web.Mvc to ASP.NET Core MVC migration
2. Implement proper HttpContext wrappers or refactor to use ASP.NET Core types directly
3. Migrate all EF6 Index attributes to EF Core Fluent API in DbContext OnModelCreating
4. Create Program.cs and Startup.cs for ASP.NET Core hosting
5. Migrate Global.asax logic to middleware pipeline
6. Test and fix remaining compilation errors systematically

### Notes

- The migration is substantial due to the deep integration with System.Web
- Many files will need manual review and updates
- Consider incremental migration: get it compiling first, then refactor for best practices
- The compatibility layer is temporary and should be replaced with proper ASP.NET Core implementations
- Database migrations may need to be regenerated for EF Core

### Build Command

```bash
cd /data/code/src
dotnet build SmartStoreNET.Minimal.sln
```

### Migration Resources

- ASP.NET Core Migration Guide: https://docs.microsoft.com/en-us/aspnet/core/migration/
- EF Core Migration Guide: https://docs.microsoft.com/en-us/ef/efcore-and-ef6/porting/
- System.Web to ASP.NET Core: https://docs.microsoft.com/en-us/aspnet/core/migration/proper-to-2x/
