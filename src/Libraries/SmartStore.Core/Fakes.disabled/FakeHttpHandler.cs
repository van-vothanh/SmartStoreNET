using System;
// using System.Web; // Removed for .NET 8 migration

namespace SmartStore.Core.Fakes
{
    public class FakeHttpHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
