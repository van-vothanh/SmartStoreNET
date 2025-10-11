using System;
using Autofac;
// TODO: Migrate to IServiceScopeFactory
// using Autofac.Integration.Mvc;

namespace SmartStore.Core.Infrastructure.DependencyManagement
{
    // TODO: Migrate to IServiceScopeFactory
    // ILifetimeScopeProvider is from Autofac.Integration.Mvc which is obsolete
    /*
    public class DefaultLifetimeScopeProvider : ILifetimeScopeProvider
    {
        private readonly ILifetimeScope _container;

        public DefaultLifetimeScopeProvider(ILifetimeScope container)
        {
            _container = container;
        }

        public ILifetimeScope ApplicationContainer => _container;

        public ILifetimeScope GetLifetimeScope(Action<ContainerBuilder> configurationAction)
        {
            return (configurationAction == null)
                ? RequestLifetime
                : RequestLifetime.BeginLifetimeScope(configurationAction);
        }

        public ILifetimeScope RequestLifetime => AutofacDependencyResolver.Current.RequestLifetimeScope;

        public void EndLifetimeScope()
        {
        }
    }
    */
}
