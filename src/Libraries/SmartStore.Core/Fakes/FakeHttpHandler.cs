using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmartStore.Core.Fakes
{
    public class FakeHttpHandler
    {
        public bool IsReusable => false;

        public virtual Task ProcessRequestAsync(HttpContext context)
        {
            return Task.CompletedTask;
        }
    }
}
