using System;
using Microsoft.AspNetCore.Http;

namespace SmartStore.Core.Infrastructure.DependencyManagement
{
    // TODO: Migrate to ASP.NET Core middleware
    // IHttpModule is not supported in ASP.NET Core
    // Request scoping is handled automatically by ASP.NET Core DI
    /*
    /// <summary>
    /// An <see cref="IHttpModule"/> and <see cref="ILifetimeScopeProvider"/> implementation 
    /// that creates a nested lifetime scope for each HTTP request.
    /// </summary>
    public class AutofacRequestLifetimeHttpModule : IHttpModule
    {
        // Original implementation commented out for migration
    }
    */

    // Stub to allow compilation
    public class AutofacRequestLifetimeHttpModule
    {
        public static void SetLifetimeScopeProvider(object lifetimeScopeProvider)
        {
            // TODO: Implement for ASP.NET Core
        }
    }
}
