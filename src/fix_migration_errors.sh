#!/bin/bash

# Comprehensive .NET 8 Migration Fix Script
# This script systematically fixes remaining compilation errors

echo "Starting comprehensive migration fixes..."

# Fix 1: Replace HttpContextBase with HttpContext
find . -name "*.cs" -type f -exec sed -i 's/HttpContextBase/HttpContext/g' {} \;
echo "✓ Replaced HttpContextBase with HttpContext"

# Fix 2: Replace HttpRequestBase with HttpRequest  
find . -name "*.cs" -type f -exec sed -i 's/HttpRequestBase/HttpRequest/g' {} \;
echo "✓ Replaced HttpRequestBase with HttpRequest"

# Fix 3: Replace HttpResponseBase with HttpResponse
find . -name "*.cs" -type f -exec sed -i 's/HttpResponseBase/HttpResponse/g' {} \;
echo "✓ Replaced HttpResponseBase with HttpResponse"

# Fix 4: Replace ILifetimeScopeProvider with IServiceScopeFactory
find . -name "*.cs" -type f -exec sed -i 's/ILifetimeScopeProvider/IServiceScopeFactory/g' {} \;
echo "✓ Replaced ILifetimeScopeProvider with IServiceScopeFactory"

# Fix 5: Replace CacheItemPriority.NotRemovable with CacheItemPriority.NeverRemove
find . -name "*.cs" -type f -exec sed -i 's/CacheItemPriority\.NotRemovable/CacheItemPriority.NeverRemove/g' {} \;
echo "✓ Replaced CacheItemPriority.NotRemovable"

# Fix 6: Comment out IRegisteredObject implementations
find . -name "*.cs" -type f -exec sed -i 's/: IRegisteredObject/\/\/ TODO: IRegisteredObject not supported - use IHostedService\n\/\/ : IRegisteredObject/g' {} \;
echo "✓ Commented out IRegisteredObject"

# Fix 7: Replace RouteValueDictionary namespace
find . -name "*.cs" -type f -exec sed -i 's/using System\.Web\.Routing;/using Microsoft.AspNetCore.Routing;/g' {} \;
echo "✓ Fixed RouteValueDictionary namespace"

# Fix 8: Add Microsoft.AspNetCore.Identity using where needed
find . -name "*Principal.cs" -type f -exec sed -i '1i using Microsoft.AspNetCore.Identity;' {} \;
echo "✓ Added Microsoft.AspNetCore.Identity using"

# Fix 9: Replace GlobalFilterCollection references
find . -name "*.cs" -type f -exec sed -i 's/GlobalFilterCollection/\/\/ TODO: GlobalFilterCollection not supported - register filters in Program.cs\n\/\/ GlobalFilterCollection/g' {} \;
echo "✓ Commented out GlobalFilterCollection"

# Fix 10: Replace IAuthenticationFilter
find . -name "*.cs" -type f -exec sed -i 's/IAuthenticationFilter/\/\/ TODO: IAuthenticationFilter not supported - use middleware\n\/\/ IAuthenticationFilter/g' {} \;
echo "✓ Commented out IAuthenticationFilter"

# Fix 11: Replace AuthenticationContext
find . -name "*.cs" -type f -exec sed -i 's/AuthenticationContext/\/\/ TODO: AuthenticationContext not supported\n\/\/ AuthenticationContext/g' {} \;
echo "✓ Commented out AuthenticationContext"

# Fix 12: Replace AuthenticationChallengeContext
find . -name "*.cs" -type f -exec sed -i 's/AuthenticationChallengeContext/\/\/ TODO: AuthenticationChallengeContext not supported\n\/\/ AuthenticationChallengeContext/g' {} \;
echo "✓ Commented out AuthenticationChallengeContext"

# Fix 13: Replace HtmlTextWriter
find . -name "*.cs" -type f -exec sed -i 's/HtmlTextWriter/\/\/ TODO: HtmlTextWriter not supported - use TagBuilder\n\/\/ HtmlTextWriter/g' {} \;
echo "✓ Commented out HtmlTextWriter"

# Fix 14: Replace CacheDependency
find . -name "*.cs" -type f -exec sed -i 's/CacheDependency/\/\/ TODO: CacheDependency not supported - use IChangeToken\n\/\/ CacheDependency/g' {} \;
echo "✓ Commented out CacheDependency"

# Fix 15: Replace AspNetHostingPermissionLevel
find . -name "*.cs" -type f -exec sed -i 's/AspNetHostingPermissionLevel/\/\/ TODO: AspNetHostingPermissionLevel not supported - always full trust in .NET 8\n\/\/ AspNetHostingPermissionLevel/g' {} \;
echo "✓ Commented out AspNetHostingPermissionLevel"

# Fix 16: Comment out NuGet.Core v2 API classes
find . -path "*/Packaging/NuGet/*.cs" -type f -exec sed -i '1i \/\/ TODO: This file uses NuGet.Core v2 API which is obsolete. Needs migration to NuGet.Protocol v3 API' {} \;
echo "✓ Added TODO comments to NuGet files"

# Fix 17: Replace IHttpModule
find . -name "*.cs" -type f -exec sed -i 's/: IHttpModule/\/\/ TODO: IHttpModule not supported - convert to middleware\n\/\/ : IHttpModule/g' {} \;
echo "✓ Commented out IHttpModule"

# Fix 18: Replace HttpApplication
find . -name "*.cs" -type f -exec sed -i 's/HttpApplication/\/\/ TODO: HttpApplication not supported\n\/\/ HttpApplication/g' {} \;
echo "✓ Commented out HttpApplication"

# Fix 19: Replace IHttpHandler
find . -name "*.cs" -type f -exec sed -i 's/: IHttpHandler/\/\/ TODO: IHttpHandler not supported - use endpoint or controller\n\/\/ : IHttpHandler/g' {} \;
echo "✓ Commented out IHttpHandler"

# Fix 20: Replace HttpCookieCollection
find . -name "*.cs" -type f -exec sed -i 's/HttpCookieCollection/\/\/ TODO: HttpCookieCollection not supported - use IRequestCookieCollection\n\/\/ HttpCookieCollection/g' {} \;
echo "✓ Commented out HttpCookieCollection"

echo "Migration fixes completed!"
echo "Run 'dotnet build SmartStoreNET.Minimal.sln' to check remaining errors"
