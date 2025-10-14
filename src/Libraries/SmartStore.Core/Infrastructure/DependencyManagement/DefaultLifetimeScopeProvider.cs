using System;
using Autofac;

namespace SmartStore.Core.Infrastructure.DependencyManagement
{
    // TODO: Update to use ASP.NET Core DI patterns
    public interface ILifetimeScopeProvider
    {
        ILifetimeScope ApplicationContainer { get; }
        void EndLifetimeScope();
        ILifetimeScope GetLifetimeScope(Action<ContainerBuilder> configurationAction);
    }

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
