using Microsoft.AspNetCore.Mvc;

namespace SmartStore.Core.Events
{
    // TODO: Migrate to Program.cs filter registration
    // GlobalFilterCollection is not available in ASP.NET Core
    // Filters are registered in Program.cs using:
    // builder.Services.AddControllersWithViews(options => { options.Filters.Add<MyFilter>(); });
    
    /*
    /// <summary>
    /// to register global filters in Application_Start
    /// </summary>
    public class AppRegisterGlobalFiltersEvent
    {
        public GlobalFilterCollection Filters { get; set; }
    }
    */
}
