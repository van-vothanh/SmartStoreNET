using System;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace SmartStore.Core.Infrastructure
{
    /// <summary>
    /// Holds some state for the current HttpContext or thread
    /// </summary>
    /// <typeparam name="T">The type of data to store</typeparam>
    public class ContextState<T> where T : class
    {
        private readonly string _name;
        private readonly Func<T> _defaultValue;
        private readonly AsyncLocal<T> _asyncLocal = new AsyncLocal<T>();
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ContextState(string name)
        {
            _name = name;
        }

        public ContextState(string name, Func<T> defaultValue)
        {
            _name = name;
            _defaultValue = defaultValue;
        }

        public T GetState()
        {
            var key = BuildKey();
            var httpContext = _httpContextAccessor?.HttpContext;

            if (httpContext == null)
            {
                var data = _asyncLocal.Value;

                if (data == null && _defaultValue != null)
                {
                    data = _defaultValue();
                    _asyncLocal.Value = data;
                }

                return data;
            }

            if (httpContext.Items[key] == null && _defaultValue != null)
            {
                httpContext.Items[key] = _defaultValue();
            }

            return httpContext.Items[key] as T;
        }

        public void SetState(T state)
        {
            var httpContext = _httpContextAccessor?.HttpContext;

            if (httpContext == null)
            {
                _asyncLocal.Value = state;
            }
            else
            {
                httpContext.Items[BuildKey()] = state;
            }
        }

        public void RemoveState()
        {
            var key = BuildKey();
            var httpContext = _httpContextAccessor?.HttpContext;

            if (httpContext == null)
            {
                _asyncLocal.Value = null;
            }
            else
            {
                httpContext.Items.Remove(key);
            }
        }

        private string BuildKey()
        {
            return "__ContextState." + _name;
        }
    }
}
