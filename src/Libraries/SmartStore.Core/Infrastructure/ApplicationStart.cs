using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Core.Logging;

namespace SmartStore.Core.Infrastructure
{
    /// <summary>
    /// Activated and executed BEFORE Application_Start. Don't use any dependencies here, they are not bootstrapped yet.
    /// Only invoke low-level code like registering an HttpModule.
    /// </summary>
    public interface IPreApplicationStart
    {
        void Start();
    }

    /// <summary>
    /// Activated and executed DURING Application_Start. Don't use request scoped dependencies here, because <see cref="HttpContext.Current"/> is still <c>null</c> at this stage.
    /// </summary>
    public interface IApplicationStart
    {
        void Start();
        int Order { get; }
    }

    /// <summary>
    /// Activated and executed once AFTER Application_Start with the very first request and very early in the request lifecycle.
    /// Invoke app initialization code here that depends on other services like IDbContext etc.
    /// </summary>
    public interface IPostApplicationStart
    {
        void Start(HttpContext httpContext);

        /// <summary>
        /// Called when an error occurred and <see cref="ThrowOnError"/> is <c>false</c>.
        /// </summary>
        /// <param name="exception">The error</param>
        /// <param name="willRetry"><c>true</c> when current attempt count is less than <see cref="MaxAttempts"/>, <c>false</c> otherwise.</param>
        void OnFail(Exception exception, bool willRetry);

        int Order { get; }

        /// <summary>
        /// Whether to throw any error and stop execution of subsequent tasks.
        /// If this is <c>false</c>, the task will be executed <see cref="MaxAttempts"/> times max when an error occurs.
        /// For every error <see cref="OnFail(Exception, bool)"/> will be invoked to give you the chance to do some logging or fix things.
        /// After that, the task will be removed from the queue.
        /// </summary>
        bool ThrowOnError { get; }

        /// <summary>
        /// The number of maximum execution attempts before this task is removed from the queue.
        /// Has no effect if <see cref="ThrowOnError"/> is <c>true</c>.
        /// </summary>
        int MaxAttempts { get; }
    }

    // TODO: Migrate PostApplicationStartFilter to ASP.NET Core middleware
    // IAuthenticationFilter is not supported in ASP.NET Core
    /*
    public sealed class PostApplicationStartFilter : IAuthenticationFilter
    {
        // Original implementation commented out for migration
    }
    */
}
