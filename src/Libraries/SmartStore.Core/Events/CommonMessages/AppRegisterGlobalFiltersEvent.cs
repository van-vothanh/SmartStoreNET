// using System.Web.Mvc; // Removed for .NET 8 migration
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace SmartStore.Core.Events
{
    /// <summary>
    /// to register global filters in Application_Start
    /// </summary>
    public class AppRegisterGlobalFiltersEvent
    {
        public IList<IFilterMetadata> Filters { get; set; } = new List<IFilterMetadata>();
    }
}
