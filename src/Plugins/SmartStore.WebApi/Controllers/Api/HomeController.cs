using Microsoft.AspNetCore.Mvc;
using SmartStore.Web.Framework.WebApi.Security;

namespace SmartStore.WebApi.Controllers.Api
{
    [WebApiAuthenticate]
    public class HomeController : ApiController
    {
    }
}
