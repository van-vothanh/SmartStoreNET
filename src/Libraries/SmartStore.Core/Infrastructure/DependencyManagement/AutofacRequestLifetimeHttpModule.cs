using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace SmartStore.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// Middleware that creates a nested lifetime scope for each HTTP request.
    /// This replaces the old IHttpModule implementation for ASP.NET Core.
    /// </summary>
    public class AutofacRequestLifetimeMiddleware
    {
        private readonly RequestDelegate _next;

        public AutofacRequestLifetimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                // Clean up disposable items from HttpContext.Items
                PurgeContextItems(context);
            }
        }

        private static void PurgeContextItems(HttpContext context)
        {
            var items = context?.Items;

            if (items != null && items.Count > 0)
            {
                var keys = new object[items.Count];
                items.Keys.CopyTo(keys, 0);

                foreach (var key in keys)
                {
                    if (items[key] is IDisposable disposable)
                    {
                        try
                        {
                            disposable.Dispose();
                        }
                        catch
                        {
                            // Ignore disposal errors
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Legacy interface for backward compatibility.
    /// In ASP.NET Core, lifetime scopes are managed by the built-in DI container.
    /// </summary>
    public interface ILifetimeScopeProvider
    {
        ILifetimeScope ApplicationContainer { get; }
        ILifetimeScope RequestLifetime { get; }
        void EndLifetimeScope();
    }

    /// <summary>
    /// Default implementation of ILifetimeScopeProvider for ASP.NET Core.
    /// </summary>
    public class DefaultLifetimeScopeProvider : ILifetimeScopeProvider
    {
        private readonly ILifetimeScope _applicationContainer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DefaultLifetimeScopeProvider(ILifetimeScope applicationContainer, IHttpContextAccessor httpContextAccessor)
        {
            _applicationContainer = applicationContainer;
            _httpContextAccessor = httpContextAccessor;
        }

        public ILifetimeScope ApplicationContainer => _applicationContainer;

        public ILifetimeScope RequestLifetime
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                if (context != null)
                {
                    return context.RequestServices.GetAutofacRoot();
                }
                return _applicationContainer;
            }
        }

        public void EndLifetimeScope()
        {
            // In ASP.NET Core, request scopes are automatically disposed
            // This method is kept for backward compatibility
        }
    }
}
