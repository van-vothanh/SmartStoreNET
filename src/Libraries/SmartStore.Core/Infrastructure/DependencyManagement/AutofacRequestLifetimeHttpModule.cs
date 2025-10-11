using System;
using Autofac;
// TODO: Migrate to ASP.NET Core middleware
// using Autofac.Integration.Mvc;

namespace SmartStore.Core.Infrastructure.DependencyManagement
{
    // TODO: Migrate to ASP.NET Core middleware
    // IHttpModule is not supported in ASP.NET Core
    // Request scoping is handled automatically by ASP.NET Core DI
    /*
    public class AutofacRequestLifetimeHttpModule : IHttpModule
    {
        public static Action<ContainerBuilder> ConfigureContainer { get; set; }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) =>
            {
                var lifetimeScope = AutofacDependencyResolver.Current.RequestLifetimeScope;
                if (ConfigureContainer != null)
                {
                    var builder = new ContainerBuilder();
                    ConfigureContainer(builder);
                    builder.Update(lifetimeScope.ComponentRegistry);
                }
            };
        }

        public void Dispose()
        {
        }
    }
    */
}
