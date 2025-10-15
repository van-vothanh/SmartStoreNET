using System;

namespace SmartStore.Core.Infrastructure.DependencyManagement
{
    // TODO: Migrate to ASP.NET Core middleware
    // IHttpModule is not supported in ASP.NET Core
    // Request scoping is handled automatically by ASP.NET Core DI
    // This class needs to be converted to middleware or removed if functionality is built-in

    /*
    /// <summary>
    /// An <see cref="IHttpModule"/> and <see cref="ILifetimeScopeProvider"/> implementation 
    /// that creates a nested lifetime scope for each HTTP request.
    /// </summary>
    public class AutofacRequestLifetimeHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            Guard.NotNull(context, nameof(context));

            context.EndRequest += OnEndRequest;
        }

        public static void OnEndRequest(object sender, EventArgs e)
        {
            if (LifetimeScopeProvider != null)
            {
                LifetimeScopeProvider.EndLifetimeScope();
            }

            // Dispose all other disposable object in HttpContext.Items
            PurgeContextItems(sender as HttpApplication);
        }

        private static void PurgeContextItems(HttpApplication app)
        {
            var items = app?.Context?.Items;

            if (items != null)
            {
                int size = items.Count;
                if (size > 0)
                {
                    var keys = new object[size];
                    items.Keys.CopyTo(keys, 0);

                    for (int i = 0; i < size; i++)
                    {
                        var obj = items[keys[i]] as IDisposable;
                        if (obj != null)
                        {
                            try
                            {
                                obj.Dispose();
                            }
                            catch { }
                        }
                    }
                }
            }
        }

        public static void SetLifetimeScopeProvider(ILifetimeScopeProvider lifetimeScopeProvider)
        {
            LifetimeScopeProvider = lifetimeScopeProvider ?? throw new ArgumentNullException("lifetimeScopeProvider");
        }


        internal static ILifetimeScopeProvider LifetimeScopeProvider
        {
            get;
            private set;
        }

        public void Dispose()
        {
        }

    }
    */

    // Stub to allow compilation
    public class AutofacRequestLifetimeHttpModule
    {
        public static void SetLifetimeScopeProvider(ILifetimeScopeProvider lifetimeScopeProvider)
        {
            // TODO: Implement for ASP.NET Core
        }

        public static void OnEndRequest(object sender, EventArgs e)
        {
            // TODO: Implement for ASP.NET Core
        }
    }
}
