// TODO: .NET 8 Migration - Fake classes need rewrite for ASP.NET Core
#if FALSE_FAKE_CLASSES_NOT_COMPATIBLE
﻿using System.Text;
using Microsoft.AspNetCore.Http;

namespace SmartStore.Core.Fakes
{
    public class FakeHttpResponse : HttpResponse
    {
        private readonly HttpCookieCollection _cookies;
        private readonly StringBuilder _outputString = new StringBuilder();

        public FakeHttpResponse()
        {
            this._cookies = new HttpCookieCollection();
        }

        public string ResponseOutput => _outputString.ToString();

        public override int StatusCode { get; set; }

        public override string RedirectLocation { get; set; }

        public override void Write(string s)
        {
            _outputString.Append(s);
        }

        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }

        public override HttpCookieCollection Cookies => _cookies;
    }
}
#endif
