using System;
using Autofac;
// TODO: ILifetimeScopeProvider removed in Autofac 8.x
// Use IServiceScopeFactory instead

namespace SmartStore.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// TODO: This class used ILifetimeScopeProvider which doesn't exist in Autofac 8.x.
    /// Migrate to use IServiceScopeFactory or remove if not needed.
    /// </summary>
    public class DefaultLifetimeScopeProvider
    {
        public DefaultLifetimeScopeProvider()
        {
            throw new NotImplementedException("TODO: ILifetimeScopeProvider removed in Autofac 8.x. Use IServiceScopeFactory instead.");
        }
    }
}

/*
// OLD IMPLEMENTATION - COMMENTED OUT FOR MIGRATION
using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace SmartStore.Core.Infrastructure.DependencyManagement
{
    public class DefaultLifetimeScopeProvider : ILifetimeScopeProvider
    {
        private readonly ILifetimeScopeAccessor _accessor;

        public DefaultLifetimeScopeProvider(ILifetimeScopeAccessor accessor)
        {
            Guard.NotNull(accessor, nameof(accessor));

            this._accessor = accessor;
            AutofacRequestLifetimeHttpModule.SetLifetimeScopeProvider(this);
        }

        public ILifetimeScope ApplicationContainer => _accessor.ApplicationContainer;

        public void EndLifetimeScope()
        {
            _accessor.EndLifetimeScope();
        }

        public ILifetimeScope GetLifetimeScope(Action<ContainerBuilder> configurationAction)
        {
            return _accessor.GetLifetimeScope(configurationAction);
        }

    }
}
*/
