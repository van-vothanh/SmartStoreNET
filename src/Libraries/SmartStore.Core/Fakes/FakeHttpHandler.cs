#if false // TODO: .NET 8 migration - temporarily disabled
﻿using System;
using System.Web;

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
