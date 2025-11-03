// Compatibility stubs for Autofac.Integration.Mvc
// TODO: Replace with proper Autofac ASP.NET Core integration

using System;
using Autofac;

namespace Autofac.Integration.Mvc
{
    public interface ILifetimeScopeProvider
    {
        ILifetimeScope ApplicationContainer { get; }
        ILifetimeScope GetLifetimeScope(Action<ContainerBuilder> configurationAction);
        void EndLifetimeScope();
    }
}
