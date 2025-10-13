using System;
// TODO: Migrate to ASP.NET Core middleware
// IHttpModule is not supported in ASP.NET Core
// Request scoping is handled automatically by ASP.NET Core DI

namespace SmartStore.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// TODO: This class used IHttpModule which is not supported in ASP.NET Core.
    /// Convert to middleware or remove if request scoping is handled by framework.
    /// </summary>
    public class AutofacRequestLifetimeHttpModule
    {
        public AutofacRequestLifetimeHttpModule()
        {
            throw new NotImplementedException("TODO: This class used IHttpModule which is not supported in ASP.NET Core. Convert to middleware.");
        }
    }
}

/*
// OLD IMPLEMENTATION - COMMENTED OUT FOR MIGRATION
using System;
using System.Web;
using Autofac.Extensions.DependencyInjection;

namespace SmartStore.Core.Infrastructure.DependencyManagement
{
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
}
*/
