using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Autofac;
using SmartStore.Core.Infrastructure;

namespace SmartStore.Core.Async
{
    using EventQueue = ConcurrentQueue<Tuple<SendOrPostCallback, object>>;

    public class AsyncRunner : IDisposable
    {
        #region Instance members

        private readonly ExclusiveSynchronizationContext CurrentContext;
        private readonly SynchronizationContext OldContext;
        private int TaskCount;

        private AsyncRunner()
        {
            OldContext = SynchronizationContext.Current;
            CurrentContext = new ExclusiveSynchronizationContext(OldContext);
            SynchronizationContext.SetSynchronizationContext(CurrentContext);
        }

        public void Dispose()
        {
            SynchronizationContext.SetSynchronizationContext(OldContext);
        }

        private void Increment()
        {
            Interlocked.Increment(ref TaskCount);
        }

        private void Decrement()
        {
            Interlocked.Decrement(ref TaskCount);
            if (TaskCount == 0)
            {
                CurrentContext.EndMessageLoop();
            }
        }

        /// <summary>
        /// Executes an async Task method which has a void return value synchronously
        /// </summary>
        /// <param name="task">Task execute</param>
        public void Run(Task task, Action<Task> continuation = null)
        {
            CurrentContext.Post(async _ =>
            {
                try
                {
                    Increment();
                    await task;

                    continuation?.Invoke(task);
                }
                catch (Exception e)
                {
                    CurrentContext.InnerException = e;
                }
                finally
                {
                    Decrement();
                }
            }, null);

            CurrentContext.BeginMessageLoop();
        }

        /// <summary>
        /// Executes an async Task method which has a TResult return type synchronously
        /// </summary>
        /// <typeparam name="TResult">Return Type</typeparam>
        /// <param name="task">Task to execute</param>
        public TResult Run<TResult>(Task<TResult> task, Action<Task<TResult>> continuation = null)
        {
            var result = default(TResult);
            var curContext = CurrentContext;

            curContext.Post(async _ =>
            {
                try
                {
                    Increment();
                    result = await task;

                    continuation?.Invoke(task);
                }
                catch (Exception e)
                {
                    CurrentContext.InnerException = e;
                }
                finally
                {
                    Decrement();
                }
            }, null);

            curContext.BeginMessageLoop();

            return result;
        }

        #endregion

        // TODO: Migrate to IHostApplicationLifetime
        // private static readonly BackgroundWorkHost _host = new BackgroundWorkHost();

        /// <summary>
        /// Gets the global cancellation token which signals the application shutdown
        /// </summary>
        public static CancellationToken AppShutdownCancellationToken => CancellationToken.None; // TODO: Use IHostApplicationLifetime.ApplicationStopping

        public static AsyncRunner Create()
        {
            return new AsyncRunner();
        }

        /// <summary>
        /// Executes an async Task method which has a void return value synchronously
        /// </summary>
        /// <param name="func">Task method to execute</param>
        public static void RunSync(Func<Task> func, Action<Task> continuation = null)
        {
            using (var runner = new AsyncRunner())
            {
                runner.Run(func(), continuation);
            }
        }

        /// <summary>
        /// Executes an async Task method which has a TResult return type synchronously
        /// </summary>
        /// <typeparam name="TResult">Return Type</typeparam>
        /// <param name="func">Task method to execute</param>
        /// <returns></returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func, Action<Task<TResult>> continuation = null)
        {
            var result = default(TResult);

            using (var runner = new AsyncRunner())
            {
                result = runner.Run(func(), continuation);
            }

            return result;
        }

        public static Task Run(
            Action<ILifetimeScope, CancellationToken> action,
            CancellationToken cancellationToken = default(CancellationToken),
            TaskCreationOptions options = TaskCreationOptions.LongRunning,
            TaskScheduler scheduler = null)
        {
            Guard.NotNull(action, nameof(action));

            var ct = _host.CreateCompositeCancellationTokenSource(cancellationToken).Token;

            var t = Task.Factory.StartNew(() =>
            {
                var accessor = EngineContext.Current.ContainerManager.ScopeAccessor;
                using (accessor.BeginContextAwareScope())
                {
                    action(accessor.GetLifetimeScope(null), ct);
                }
            }, ct, options, scheduler ?? TaskScheduler.Default);

            _host.Register(t, ct);

            return t;
        }

        public static Task Run(
            Action<ILifetimeScope, CancellationToken, object> action,
            object state,
            CancellationToken cancellationToken = default(CancellationToken),
            TaskCreationOptions options = TaskCreationOptions.LongRunning,
            TaskScheduler scheduler = null)
        {
            Guard.NotNull(state, nameof(state));
            Guard.NotNull(action, nameof(action));

            var ct = _host.CreateCompositeCancellationTokenSource(cancellationToken).Token;

            var t = Task.Factory.StartNew((o) =>
            {
                var accessor = EngineContext.Current.ContainerManager.ScopeAccessor;
                using (accessor.BeginContextAwareScope())
                {
                    action(accessor.GetLifetimeScope(null), ct, o);
                }
            }, state, ct, options, scheduler ?? TaskScheduler.Default);

            _host.Register(t, ct);

            return t;
        }

        public static Task<TResult> Run<TResult>(
            Func<ILifetimeScope, CancellationToken, TResult> function,
            CancellationToken cancellationToken = default(CancellationToken),
            TaskCreationOptions options = TaskCreationOptions.LongRunning,
            TaskScheduler scheduler = null)
        {
            Guard.NotNull(function, nameof(function));

            var ct = _host.CreateCompositeCancellationTokenSource(cancellationToken).Token;

            var t = Task.Factory.StartNew(() =>
            {
                var accessor = EngineContext.Current.ContainerManager.ScopeAccessor;
                using (accessor.BeginContextAwareScope())
                {
                    return function(accessor.GetLifetimeScope(null), ct);
                }
            }, ct, options, scheduler ?? TaskScheduler.Default);

            _host.Register(t, ct);

            return t;
        }

        public static Task<TResult> Run<TResult>(
            Func<ILifetimeScope, CancellationToken, object, TResult> function,
            object state,
            CancellationToken cancellationToken = default(CancellationToken),
            TaskCreationOptions options = TaskCreationOptions.LongRunning,
            TaskScheduler scheduler = null)
        {
            Guard.NotNull(state, nameof(state));
            Guard.NotNull(function, nameof(function));

            var ct = _host.CreateCompositeCancellationTokenSource(cancellationToken).Token;

            var t = Task.Factory.StartNew((o) =>
            {
                var accessor = EngineContext.Current.ContainerManager.ScopeAccessor;
                using (accessor.BeginContextAwareScope())
                {
                    return function(accessor.GetLifetimeScope(null), ct, o);
                }
            }, state, ct, options, scheduler ?? TaskScheduler.Default);

            _host.Register(t, ct);

            return t;
        }

        public static Task Run(
            Func<ILifetimeScope, CancellationToken, Task> function,
            object state,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Guard.NotNull(state, nameof(state));
            Guard.NotNull(function, nameof(function));

            var ct = _host.CreateCompositeCancellationTokenSource(cancellationToken).Token;

            var accessor = EngineContext.Current.ContainerManager.ScopeAccessor;
            var scope = accessor.BeginLifetimeScope(null);

            Task task = null;

            try
            {
                task = function(scope, ct).ContinueWith(x =>
                {
                    scope.Dispose();
                });
                _host.Register(task, ct);
            }
            catch
            {
                scope.Dispose();
            }

            return task;
        }

        private class ExclusiveSynchronizationContext : SynchronizationContext
        {
            private bool _done;
            private readonly AutoResetEvent _workItemsWaiting = new AutoResetEvent(false);
            private readonly EventQueue _items;

            public ExclusiveSynchronizationContext(SynchronizationContext old)
            {
                if (old is ExclusiveSynchronizationContext oldEx)
                {
                    this._items = oldEx._items;
                }
                else
                {
                    this._items = new EventQueue();
                }
            }

            public Exception InnerException { get; set; }

            public override void Send(SendOrPostCallback d, object state)
            {
                throw new NotSupportedException("We cannot send to the same thread");
            }

            public override void Post(SendOrPostCallback d, object state)
            {
                _items.Enqueue(Tuple.Create(d, state));
                _workItemsWaiting.Set();
            }

            public void EndMessageLoop()
            {
                Post(_ => _done = true, null);
            }

            public void BeginMessageLoop()
            {
                while (!_done)
                {
                    if (!_items.TryDequeue(out var task))
                    {
                        _workItemsWaiting.WaitOne();
                    }
                    else
                    {
                        task.Item1(task.Item2);

                        if (InnerException != null) // method threw an exeption
                        {
                            throw new AggregateException("AsyncHelpers.Run method threw an exception.", InnerException);
                        }
                    }
                }
            }

            public override SynchronizationContext CreateCopy()
            {
                return this;
            }
        }
    }

// TODO: Migrate to IHostedService
//     internal class BackgroundWorkHost : IRegisteredObject
// TODO: Migrate to IHostedService
//     {
// TODO: Migrate to IHostedService
//         private readonly CancellationTokenSource _shutdownCancellationTokenSource = new CancellationTokenSource();
// TODO: Migrate to IHostedService
//         private int _numRunningWorkItems;
// TODO: Migrate to IHostedService
// 
// TODO: Migrate to IHostedService
//         public BackgroundWorkHost()
// TODO: Migrate to IHostedService
//         {
// TODO: Migrate to IHostedService
//             HostingEnvironment.RegisterObject(this);
// TODO: Migrate to IHostedService
//         }
// TODO: Migrate to IHostedService
// 
// TODO: Migrate to IHostedService
//         public CancellationTokenSource ShutdownCancellationTokenSource => _shutdownCancellationTokenSource;
// TODO: Migrate to IHostedService
// 
// TODO: Migrate to IHostedService
//         public void Stop(bool immediate)
// TODO: Migrate to IHostedService
//         {
// TODO: Migrate to IHostedService
//             int num;
// TODO: Migrate to IHostedService
//             lock (this)
// TODO: Migrate to IHostedService
//             {
// TODO: Migrate to IHostedService
//                 _shutdownCancellationTokenSource.Cancel();
// TODO: Migrate to IHostedService
//                 num = _numRunningWorkItems;
// TODO: Migrate to IHostedService
//             }
// TODO: Migrate to IHostedService
//             if (num == 0)
// TODO: Migrate to IHostedService
//             {
// TODO: Migrate to IHostedService
//                 FinalShutdown();
// TODO: Migrate to IHostedService
//             }
// TODO: Migrate to IHostedService
//         }
// TODO: Migrate to IHostedService
// 
// TODO: Migrate to IHostedService
//         public CancellationTokenSource CreateCompositeCancellationTokenSource(CancellationToken userCancellationToken)
// TODO: Migrate to IHostedService
//         {
// TODO: Migrate to IHostedService
//             if (userCancellationToken == CancellationToken.None)
// TODO: Migrate to IHostedService
//             {
// TODO: Migrate to IHostedService
//                 return _shutdownCancellationTokenSource;
// TODO: Migrate to IHostedService
//             }
// TODO: Migrate to IHostedService
//             return CancellationTokenSource.CreateLinkedTokenSource(_shutdownCancellationTokenSource.Token, userCancellationToken);
// TODO: Migrate to IHostedService
//         }
// TODO: Migrate to IHostedService
// 
// TODO: Migrate to IHostedService
//         public void Register(Task work, CancellationToken cancellationToken)
// TODO: Migrate to IHostedService
//         {
// TODO: Migrate to IHostedService
//             if (!cancellationToken.IsCancellationRequested)
// TODO: Migrate to IHostedService
//             {
// TODO: Migrate to IHostedService
//                 lock (this)
// TODO: Migrate to IHostedService
//                 {
// TODO: Migrate to IHostedService
//                     if (cancellationToken.IsCancellationRequested)
// TODO: Migrate to IHostedService
//                     {
// TODO: Migrate to IHostedService
//                         return;
// TODO: Migrate to IHostedService
//                     }
// TODO: Migrate to IHostedService
//                     _numRunningWorkItems++;
// TODO: Migrate to IHostedService
//                 }
// TODO: Migrate to IHostedService
// 
// TODO: Migrate to IHostedService
//                 work.ContinueWith(
// TODO: Migrate to IHostedService
//                     WorkItemComplete,
// TODO: Migrate to IHostedService
//                     CancellationToken.None,
// TODO: Migrate to IHostedService
//                     TaskContinuationOptions.ExecuteSynchronously,
// TODO: Migrate to IHostedService
//                     TaskScheduler.Default);
// TODO: Migrate to IHostedService
//             }
// TODO: Migrate to IHostedService
//         }
// TODO: Migrate to IHostedService
// 
// TODO: Migrate to IHostedService
//         private void WorkItemComplete(Task work)
// TODO: Migrate to IHostedService
//         {
// TODO: Migrate to IHostedService
//             int num;
// TODO: Migrate to IHostedService
//             bool isCancellationRequested;
// TODO: Migrate to IHostedService
//             lock (this)
// TODO: Migrate to IHostedService
//             {
// TODO: Migrate to IHostedService
//                 num = --_numRunningWorkItems;
// TODO: Migrate to IHostedService
//                 isCancellationRequested = _shutdownCancellationTokenSource.IsCancellationRequested;
// TODO: Migrate to IHostedService
//             }
// TODO: Migrate to IHostedService
//             if (num == 0 && isCancellationRequested)
// TODO: Migrate to IHostedService
//             {
// TODO: Migrate to IHostedService
//                 FinalShutdown();
// TODO: Migrate to IHostedService
//             }
// TODO: Migrate to IHostedService
//         }
// TODO: Migrate to IHostedService
// 
// TODO: Migrate to IHostedService
//         private void FinalShutdown()
// TODO: Migrate to IHostedService
//         {
// TODO: Migrate to IHostedService
//             HostingEnvironment.UnregisterObject(this);
// TODO: Migrate to IHostedService
//         }
// TODO: Migrate to IHostedService
// 
// TODO: Migrate to IHostedService
//     }
}