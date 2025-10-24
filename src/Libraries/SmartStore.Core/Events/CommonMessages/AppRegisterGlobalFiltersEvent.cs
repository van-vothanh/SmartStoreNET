// TODO: .NET 8 Migration - GlobalFilterCollection not supported
#if FALSE_NOT_COMPATIBLE
﻿using Microsoft.AspNetCore.Mvc;

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
