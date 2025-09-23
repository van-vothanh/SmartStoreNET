using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace SmartStore.Core.Fakes
{
    public class FakeHttpRequest : HttpRequest
    {
        private readonly HttpContext _context;
        private readonly IQueryCollection _query;
        private readonly IFormCollection _form;
        private readonly IHeaderDictionary _headers;
        private readonly IRequestCookieCollection _cookies;

        public FakeHttpRequest(HttpContext context)
        {
            _context = context;
            _query = new QueryCollection();
            _form = new FormCollection(new Dictionary<string, StringValues>());
            _headers = new HeaderDictionary();
            _cookies = new RequestCookieCollection();
        }

        public override HttpContext HttpContext => _context;

        public override string Method { get; set; } = "GET";

        public override string Scheme { get; set; } = "http";

        public override bool IsHttps { get; set; } = false;

        public override HostString Host { get; set; } = new HostString("localhost");

        public override PathString PathBase { get; set; } = new PathString();

        public override PathString Path { get; set; } = new PathString("/");

        public override QueryString QueryString { get; set; } = new QueryString();

        public override IQueryCollection Query
        {
            get => _query;
            set => throw new NotImplementedException();
        }

        public override string Protocol { get; set; } = "HTTP/1.1";

        public override IHeaderDictionary Headers => _headers;

        public override IRequestCookieCollection Cookies
        {
            get => _cookies;
            set => throw new NotSupportedException("Setting cookies collection is not supported");
        }

        public override long? ContentLength { get; set; }

        public override string ContentType { get; set; }

        public override Stream Body { get; set; } = new MemoryStream();

        public override bool HasFormContentType => false;

        public override IFormCollection Form
        {
            get => _form;
            set => throw new NotImplementedException();
        }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_form);
        }
    }
}
