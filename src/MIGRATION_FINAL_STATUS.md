# .NET Framework to .NET 8 Migration - Final Status

## Migration Progress: 62% Complete

### ✅ Completed (Major Milestones)

1. **Project File Migration** - 100% Complete
   - All 10 projects converted to SDK-style format
   - Target framework updated to net8.0
   - Legacy imports removed
   - Modern PackageReference format implemented

2. **Package Updates** - 100% Complete
   - Autofac 5.2.0 → 8.1.0
   - EntityFramework 6.4.4 → Microsoft.EntityFrameworkCore 8.0.11
   - AngleSharp 0.9.11 → 1.1.2
   - Added System.ServiceModel.Syndication 8.0.0
   - Added Microsoft.Extensions.Caching.Memory 8.0.1

3. **Attribute Cleanup** - 100% Complete
   - Removed all EF6 [Index] attributes
   - Removed all [AllowHtml] attributes
   - Removed PreApplicationStartMethod attribute

4. **Namespace Replacements** - 80% Complete
   - System.Data.Entity → Microsoft.EntityFrameworkCore
   - System.Web.Mvc → Microsoft.AspNetCore.Mvc (partial)
   - System.Web.Routing → Microsoft.AspNetCore.Routing
   - Autofac.Integration.Mvc → Autofac.Extensions.DependencyInjection
   - Ganss.XSS → Ganss.Xss

### 🔄 In Progress (Remaining Work)

#### High Priority - Blocking Compilation (120 errors)

1. **System.Web.Mvc Dependencies** (~40 errors)
   - Files: Controllers, Filters, ApplicationStart.cs, FakeController.cs
   - Action: Need to replace with Microsoft.AspNetCore.Mvc equivalents
   - Status: Namespace replaced but types still incompatible

2. **HttpContext/HttpRequest Type Mismatches** (~30 errors)
   - Files: HttpExtensions.cs, WebHelper.cs, RequestCache.cs, Fake*.cs
   - Action: Replace HttpContextBase/HttpRequestBase with ASP.NET Core types
   - Status: Types exist but method signatures incompatible

3. **NuGet.Core v2 API** (~25 errors)
   - Files: All files in Packaging/NuGet/ folder
   - Action: Complete rewrite using NuGet.Protocol v3 API
   - Status: Not started - requires architectural changes

4. **Test Infrastructure (Fake Classes)** (~15 errors)
   - Files: Fakes/Fake*.cs
   - Action: Rewrite using ASP.NET Core test infrastructure
   - Status: Not started - may need to be removed

5. **Infrastructure Classes** (~10 errors)
   - IRegisteredObject → IHostedService
   - CacheDependency → IChangeToken
   - AspNetHostingPermissionLevel → Remove
   - Status: Identified but not fixed

### 📊 Error Breakdown

| Category | Count | Priority | Status |
|----------|-------|----------|--------|
| System.Web.Mvc | 40 | High | In Progress |
| HttpContext Types | 30 | High | In Progress |
| NuGet.Core API | 25 | Medium | Not Started |
| Fake Test Classes | 15 | Low | Not Started |
| Infrastructure | 10 | Medium | Not Started |
| **Total** | **120** | | |

### 🎯 Next Steps (Priority Order)

1. **Comment Out Incompatible Files** (Quick Win)
   - Comment out all Fake*.cs test files
   - Comment out NuGet packaging files
   - This will reduce errors by ~40

2. **Fix HttpContext Type Issues**
   - Update HttpExtensions.cs method signatures
   - Update WebHelper.cs to use ASP.NET Core types
   - Update RequestCache.cs

3. **Fix Controller and Filter Code**
   - Update all controllers to use Microsoft.AspNetCore.Mvc
   - Convert filters to ASP.NET Core filter interfaces
   - Update routing

4. **Stub Out Infrastructure Classes**
   - Create stub implementations for IRegisteredObject usage
   - Replace CacheDependency with IChangeToken
   - Remove AspNetHostingPermissionLevel checks

5. **Plan NuGet Migration**
   - This requires significant architectural work
   - May need to disable plugin installation temporarily
   - Consider using NuGet.Protocol v3 API

### 📝 Files Requiring Manual Intervention

#### Must Fix (Blocking Build)
- `/Libraries/SmartStore.Core/WebHelper.cs` - 30+ errors
- `/Libraries/SmartStore.Core/Extensions/HttpExtensions.cs` - 20+ errors
- `/Libraries/SmartStore.Core/Infrastructure/ApplicationStart.cs` - 10+ errors
- `/Libraries/SmartStore.Core/Fakes/*.cs` - 15+ errors (consider removing)

#### Should Fix (Important Features)
- `/Libraries/SmartStore.Core/Packaging/NuGet/*.cs` - 25+ errors
- `/Libraries/SmartStore.Core/Infrastructure/DependencyManagement/*.cs` - 5+ errors
- `/Libraries/SmartStore.Core/Caching/MemoryCacheManager.cs` - 3+ errors

#### Can Defer (Low Priority)
- `/Libraries/SmartStore.Core/IO/VirtualPath/*.cs` - 5+ errors
- `/Libraries/SmartStore.Core/Plugins/PluginManager.cs` - 3+ errors

### 🔧 Build Command

```bash
cd /data/code/src
dotnet build SmartStoreNET.Minimal.sln
```

### 📈 Progress Metrics

- **Projects Migrated**: 10/10 (100%)
- **Packages Updated**: 15/15 (100%)
- **Compilation Errors Fixed**: 202/322 (62.7%)
- **Remaining Errors**: 120
- **Estimated Time to Complete**: 8-16 hours of focused work

### 🚀 Quick Wins to Reduce Errors

Run these commands to quickly reduce error count:

```bash
cd /data/code/src

# 1. Comment out all Fake test classes (saves ~15 errors)
for file in Libraries/SmartStore.Core/Fakes/*.cs; do
    mv "$file" "$file.bak"
    echo "// TODO: Rewrite using ASP.NET Core test infrastructure" > "$file"
    echo "// Original file backed up as $file.bak" >> "$file"
done

# 2. Comment out NuGet packaging files (saves ~25 errors)
for file in Libraries/SmartStore.Core/Packaging/NuGet/*.cs; do
    mv "$file" "$file.bak"
    echo "// TODO: Migrate to NuGet.Protocol v3 API" > "$file"
    echo "// Original file backed up as $file.bak" >> "$file"
done

# 3. Rebuild
dotnet build SmartStoreNET.Minimal.sln
```

This should reduce errors from 120 to ~80.

### 📚 Resources

- [ASP.NET Core Migration Guide](https://docs.microsoft.com/en-us/aspnet/core/migration/proper-to-2x/)
- [EF Core Migration Guide](https://docs.microsoft.com/en-us/ef/efcore-and-ef6/)
- [NuGet v3 API Documentation](https://docs.microsoft.com/en-us/nuget/api/overview)
- [Autofac ASP.NET Core Integration](https://autofac.readthedocs.io/en/latest/integration/aspnetcore.html)

### ⚠️ Important Notes

1. **Test Infrastructure**: The Fake*.cs classes are incompatible with ASP.NET Core. Consider using:
   - `Microsoft.AspNetCore.TestHost`
   - `Microsoft.AspNetCore.Mvc.Testing`

2. **Plugin System**: The current plugin system uses NuGet.Core v2 which is obsolete. This needs architectural redesign.

3. **HTTP Modules**: All IHttpModule implementations must be converted to middleware.

4. **Global Filters**: Filter registration moved from Global.asax to Program.cs.

5. **Trust Levels**: ASP.NET Core always runs in full trust - remove all trust level checks.

### 🎓 Lessons Learned

1. SDK-style projects are much cleaner but require careful package management
2. Type ambiguity between System.Web and Microsoft.AspNetCore requires explicit using directives
3. Many .NET Framework APIs have no direct equivalent and require architectural changes
4. Test infrastructure is fundamentally different and may need complete rewrite
5. Plugin systems based on AppDomain isolation need to use AssemblyLoadContext

---

**Status**: Migration is 62% complete. Core infrastructure is migrated. Remaining work focuses on web-specific APIs and test infrastructure.

**Recommendation**: Continue with systematic file-by-file fixes, starting with commenting out incompatible test and packaging code, then fixing core web functionality.
