using System.Web;

namespace SmartStore.Core.Events
{
    public class AppStartedEvent
    {
        public HttpContext HttpContext { get; set; }
    }
}
