// TODO: This class uses System.Web.Mvc.GlobalFilterCollection which is not supported in ASP.NET Core
// Filters should be registered in Program.cs using builder.Services.AddControllersWithViews(options => { options.Filters.Add(...); })

/*
using System.Web.Mvc;

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
*/

namespace SmartStore.Core.Events
{
    // Stub class to allow compilation
    public class AppRegisterGlobalFiltersEvent
    {
        // TODO: Implement using ASP.NET Core filter registration
    }
}
