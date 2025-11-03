using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;

namespace SmartStore.Core.Async
{
    public partial class LocalAsyncState : IAsyncState
    {
        private readonly IMemoryCache _states = new MemoryCache(new MemoryCacheOptions());
        private readonly IMemoryCache _cancelTokens = new MemoryCache(new MemoryCacheOptions());

        public virtual bool Exists<T>(string name = null)
        {
            var key = BuildKey<T>(name);
            return _states.TryGetValue(key, out var value) && value != null && !object.Equals(((AsyncStateInfo)value).Progress, default(T));
        }

        public virtual T Get<T>(string name = null)
        {
            var key = BuildKey<T>(name);
            if (_states.TryGetValue(key, out var value) && value is AsyncStateInfo info)
            {
                return (T)info.Progress;
            }

            return default(T);
        }

        public virtual IEnumerable<T> GetAll<T>()
        {
            // Note: IMemoryCache doesn't support enumeration in .NET Core
            // This would need to be tracked separately if needed
            return Enumerable.Empty<T>();
        }


        public virtual void Set<T>(T state, string name = null, bool neverExpires = false)
        {
            Guard.NotNull(state, nameof(state));

            var key = BuildKey<T>(name);
            var duration = neverExpires ? TimeSpan.Zero : TimeSpan.FromMinutes(15);

            var options = new MemoryCacheEntryOptions();
            if (!neverExpires)
            {
                options.SlidingExpiration = duration;
            }
            options.Priority = CacheItemPriority.NeverRemove;
            options.RegisterPostEvictionCallback((k, v, r, s) => OnRemoveCancelTokenSource((string)k));

            _states.Set(key, new AsyncStateInfo { Progress = state, Duration = duration }, options);
        }

        public virtual void Update<T>(Action<T> update, string name = null)
        {
            Guard.NotNull(update, nameof(update));

            var key = BuildKey<T>(name);
            if (_states.TryGetValue(key, out var value) && value is AsyncStateInfo info)
            {
                var state = (T)info.Progress;
                if (state != null)
                {
                    update(state);
                }
            }
        }

        public virtual bool Remove<T>(string name = null)
        {
            var key = BuildKey<T>(name);

            if (!OnRemoveStateInfo(key))
            {
                // Remove corresponding cancel token (if any).
                // Because the state does not exist, "RemovedCallBack"
                // did not run.
                OnRemoveCancelTokenSource(key);
                return false;
            }

            return true;
        }

        protected virtual bool OnRemoveStateInfo(string key)
        {
            _states.Remove(key);
            return true;
        }

        public virtual bool RemoveCancelTokenSource<T>(string name = null)
        {
            return OnRemoveCancelTokenSource(BuildKey<T>(name));
        }

        protected virtual bool OnRemoveCancelTokenSource(string key, bool successive = false)
        {
            Guard.NotEmpty(key, nameof(key));

            if (_cancelTokens.TryGetValue(key, out var value) && value is CancellationTokenSource token)
            {
                _cancelTokens.Remove(key);
                token.Dispose();
                return true;
            }

            return false;
        }


        public CancellationTokenSource GetCancelTokenSource<T>(string name = null)
        {
            return OnGetCancelTokenSource(BuildKey<T>(name));
        }

        protected virtual CancellationTokenSource OnGetCancelTokenSource(string key, bool successive = false)
        {
            Guard.NotEmpty(key, nameof(key));

            if (_cancelTokens.TryGetValue(key, out var value) && value is CancellationTokenSource token)
            {
                return token;
            }

            return null;
        }

        public virtual void SetCancelTokenSource<T>(CancellationTokenSource cancelTokenSource, string name = null)
        {
            Guard.NotNull(cancelTokenSource, nameof(cancelTokenSource));

            var key = BuildKey<T>(name);

            if (Exists<T>(name))
            {
                OnRemoveCancelTokenSource(key);
            }

            var options = new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            };

            _cancelTokens.Set(key, cancelTokenSource, options);
        }

        public bool Cancel<T>(string name = null)
        {
            return OnCancel(BuildKey<T>(name));
        }

        protected virtual bool OnCancel(string key, bool successive = false)
        {
            Guard.NotEmpty(key, nameof(key));

            var cts = OnGetCancelTokenSource(key);

            if (cts != null)
            {
                cts.Cancel();
                return true;
            }

            return false;
        }


        protected virtual AsyncStateInfo GetStateInfo<T>(string name = null)
        {
            var key = BuildKey<T>(name);
            if (_states.TryGetValue(key, out var value) && value is AsyncStateInfo info)
            {
                return info;
            }
            return null;
        }

        protected string BuildKey<T>(string name)
        {
            return BuildKey(typeof(T), name);
        }

        protected virtual string BuildKey(Type type, string name)
        {
            return "{0}{1}".FormatInvariant(type.FullName, name.HasValue() ? ":" + name : "");
        }
    }
}
