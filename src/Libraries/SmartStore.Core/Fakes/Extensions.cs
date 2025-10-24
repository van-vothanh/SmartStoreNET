// TODO: .NET 8 Migration - Fake classes need rewrite for ASP.NET Core
#if FALSE_FAKE_CLASSES_NOT_COMPATIBLE
﻿using System;
using Microsoft.AspNetCore.Http;

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
#endif
