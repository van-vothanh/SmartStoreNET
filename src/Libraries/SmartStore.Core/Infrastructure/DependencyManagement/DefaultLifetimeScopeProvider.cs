#if false // TODO: Migrate to IServiceScopeFactory
﻿using System;
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
#endif
