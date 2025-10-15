using Microsoft.AspNetCore.Mvc;

namespace SmartStore.Core.Events
{
    /// <summary>
    /// to register global filters in Application_Start
    /// </summary>
    // TODO: GlobalFilterCollection not supported in .NET 8 - register filters in Program.cs
    public class AppRegisterGlobalFiltersEvent
    {
        // public GlobalFilterCollection Filters { get; set; }
    }
}
