using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmartStore.Core.Fakes
{
    public class FakeHttpResponse : HttpResponse
    {
        private readonly HttpContext _context;
        private readonly IHeaderDictionary _headers;
        private readonly IResponseCookies _cookies;

        public FakeHttpResponse(HttpContext context)
        {
            _context = context;
            _headers = new HeaderDictionary();
            _cookies = new FakeResponseCookies();
            Body = new MemoryStream();
        }

        public override HttpContext HttpContext => _context;

        public override int StatusCode { get; set; } = 200;

        public override IHeaderDictionary Headers => _headers;

        public override Stream Body { get; set; }

        public override long? ContentLength { get; set; }

        public override string ContentType { get; set; }

        public override IResponseCookies Cookies => _cookies;

        public override bool HasStarted => false;

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void Redirect(string location, bool permanent)
        {
            StatusCode = permanent ? 301 : 302;
            Headers["Location"] = location;
        }
    }

    public class FakeResponseCookies : IResponseCookies
    {
        public void Append(string key, string value)
        {
        }

        public void Append(string key, string value, CookieOptions options)
        {
        }

        public void Delete(string key)
        {
        }

        public void Delete(string key, CookieOptions options)
        {
        }
    }
}
