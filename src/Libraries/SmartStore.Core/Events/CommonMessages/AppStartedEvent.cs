#if FALSE // TODO: Migrate to ASP.NET Core startup
﻿using System.Web;

namespace SmartStore.Core.Events
{
    public class AppStartedEvent
    {
        public HttpContextBase HttpContext { get; set; }
    }
}
#endif
