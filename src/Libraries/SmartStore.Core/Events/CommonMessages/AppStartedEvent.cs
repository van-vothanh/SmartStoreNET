#if false // Disabled for .NET 8 migration
﻿using System.Web;

namespace SmartStore.Core.Events
{
    public class AppStartedEvent
    {
        public HttpContextBase HttpContext { get; set; }
    }
}
#endif
