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
            var httpContext = GetHttpContext();

            if (httpContext == null)
            {
                var data = _asyncLocal.Value;

                if (data == null && _defaultValue != null)
                {
                    _asyncLocal.Value = data = _defaultValue();
                }

                return data;
            }

            if (httpContext.Items[key] == null)
            {
                httpContext.Items[key] = _defaultValue?.Invoke();
            }

            return httpContext.Items[key] as T;
        }

        public void SetState(T state)
        {
            var httpContext = GetHttpContext();
            
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
            var httpContext = GetHttpContext();

            if (httpContext == null)
            {
                _asyncLocal.Value = null;
            }
            else
            {
                if (httpContext.Items.ContainsKey(key))
                {
                    httpContext.Items.Remove(key);
                }
            }
        }

        private string BuildKey()
        {
            return "__ContextState." + _name;
        }

        private HttpContext GetHttpContext()
        {
            // Try to get HttpContext from EngineContext
            try
            {
                var httpContextAccessor = EngineContext.Current?.Resolve<IHttpContextAccessor>();
                return httpContextAccessor?.HttpContext;
            }
            catch
            {
                return null;
            }
        }
    }
}
