using Microsoft.AspNetCore.Routing;
using SmartStore.Web.Framework.Modelling;

namespace SmartStore.Web.Models.Customer
{
    public partial class ExternalAuthenticationMethodModel : ModelBase
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
    }
}