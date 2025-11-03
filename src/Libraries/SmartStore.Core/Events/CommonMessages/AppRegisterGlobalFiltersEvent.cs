#if FALSE // TODO: Migrate to ASP.NET Core startup
﻿using System.Web.Mvc;

namespace SmartStore.Core.Events
{
    /// <summary>
    /// to register global filters in Application_Start
    /// </summary>
    public class AppRegisterGlobalFiltersEvent
    {
        public GlobalFilterCollection Filters { get; set; }
    }
}
#endif
