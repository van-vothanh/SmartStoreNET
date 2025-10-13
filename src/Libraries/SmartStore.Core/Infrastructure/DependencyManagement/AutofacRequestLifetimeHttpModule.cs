using System;
// TODO: Migrate to ASP.NET Core middleware
// IHttpModule is not supported in ASP.NET Core
// Request scoping is handled automatically by ASP.NET Core DI

namespace SmartStore.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// TODO: Convert to ASP.NET Core middleware
    /// Original implementation used IHttpModule which is not supported in ASP.NET Core
    /// </summary>
    public class AutofacRequestLifetimeHttpModule
    {
        public AutofacRequestLifetimeHttpModule()
        {
            // Stub - request scoping handled by ASP.NET Core DI
        }

        public static void OnEndRequest(object sender, EventArgs e)
        {
            // TODO: Migrate to middleware if needed
        }

        public static void SetLifetimeScopeProvider(ILifetimeScopeProvider lifetimeScopeProvider)
        {
            LifetimeScopeProvider = lifetimeScopeProvider ?? throw new ArgumentNullException("lifetimeScopeProvider");
        }

        internal static ILifetimeScopeProvider LifetimeScopeProvider { get; private set; }
    }
}
