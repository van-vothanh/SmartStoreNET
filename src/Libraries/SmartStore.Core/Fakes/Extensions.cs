// TODO: Migrate to ASP.NET Core - Fake classes need complete rewrite
// Use DefaultHttpContext and related ASP.NET Core test utilities instead
/*
﻿using System;
using System.Web;

namespace SmartStore.Core.Fakes
{
    public static class Extentions
    {
        /// <summary>
        /// Indicates whether this context is fake
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <returns>Result</returns>
        public static bool IsFakeContext(this HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            return httpContext is FakeHttpContext;
        }

    }
}
*/
