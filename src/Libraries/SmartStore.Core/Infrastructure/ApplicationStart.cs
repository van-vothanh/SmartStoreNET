using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SmartStore.Core.Infrastructure
{
    public interface IApplicationStarter
    {
        void Start();
        Task StartAsync();
    }

    public interface IStarterModule
    {
        void Start();
        Task StartAsync();
        int Order { get; }
        bool ThrowOnError { get; }
        int MaxAttempts { get; }
    }

    public class ApplicationStarter : IApplicationStarter
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ApplicationStarter> _logger;
        private static readonly object _lock = new object();
        private static bool _initialized = false;
        private static List<StarterModuleInfo> _starterModuleInfos;

        public ApplicationStarter(IServiceProvider serviceProvider, ILogger<ApplicationStarter> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void Start()
        {
            StartAsync().GetAwaiter().GetResult();
        }

        public async Task StartAsync()
        {
            if (_initialized)
                return;

            lock (_lock)
            {
                if (_initialized)
                    return;

                try
                {
                    await InitializeStarterModulesAsync();
                    _initialized = true;
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Error during application startup");
                    throw;
                }
            }
        }

        private async Task InitializeStarterModulesAsync()
        {
            if (_starterModuleInfos == null)
            {
                _starterModuleInfos = DiscoverStarterModules();
            }

            foreach (var moduleInfo in _starterModuleInfos.OrderBy(x => x.Order))
            {
                var attempts = 0;
                var maxAttempts = moduleInfo.MaxAttempts;

                while (attempts < maxAttempts)
                {
                    try
                    {
                        attempts++;
                        var module = (IStarterModule)Activator.CreateInstance(moduleInfo.Type);
                        
                        if (module != null)
                        {
                            await module.StartAsync();
                        }
                        
                        break; // Success, exit retry loop
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogWarning(ex, "Starter module {ModuleType} failed on attempt {Attempt}/{MaxAttempts}", 
                            moduleInfo.Type.Name, attempts, maxAttempts);

                        if (moduleInfo.ThrowOnError || attempts >= maxAttempts)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        private static List<StarterModuleInfo> DiscoverStarterModules()
        {
            var moduleInfos = new List<StarterModuleInfo>();

            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic && !a.GlobalAssemblyCache)
                    .ToArray();

                foreach (var assembly in assemblies)
                {
                    try
                    {
                        var moduleTypes = assembly.GetTypes()
                            .Where(t => typeof(IStarterModule).IsAssignableFrom(t) && 
                                       !t.IsInterface && 
                                       !t.IsAbstract)
                            .ToArray();

                        foreach (var moduleType in moduleTypes)
                        {
                            try
                            {
                                var tempInstance = (IStarterModule)Activator.CreateInstance(moduleType);
                                moduleInfos.Add(new StarterModuleInfo
                                {
                                    Type = moduleType,
                                    Order = tempInstance.Order,
                                    ThrowOnError = tempInstance.ThrowOnError,
                                    MaxAttempts = tempInstance.MaxAttempts
                                });
                            }
                            catch
                            {
                                // Skip modules that can't be instantiated
                            }
                        }
                    }
                    catch
                    {
                        // Skip assemblies that can't be processed
                    }
                }
            }
            catch
            {
                // Return empty list if discovery fails
            }

            return moduleInfos;
        }
    }

    internal class StarterModuleInfo
    {
        public Type Type { get; set; }
        public int Order { get; set; }
        public bool ThrowOnError { get; set; }
        public int MaxAttempts { get; set; }
    }

    /// <summary>
    /// Middleware to handle application startup initialization
    /// </summary>
    public class ApplicationStartupMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApplicationStarter _starter;

        public ApplicationStartupMiddleware(RequestDelegate next, IApplicationStarter starter)
        {
            _next = next;
            _starter = starter;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _starter.StartAsync();
            await _next(context);
        }
    }
}
