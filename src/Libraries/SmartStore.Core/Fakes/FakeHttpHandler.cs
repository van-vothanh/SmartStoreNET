// TODO: .NET 8 Migration - Fake classes need rewrite for ASP.NET Core
#if FALSE_FAKE_CLASSES_NOT_COMPATIBLE
﻿using System;
using Microsoft.AspNetCore.Http;

namespace SmartStore.Core.Fakes
{
    public class FakeHttpHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
#endif
