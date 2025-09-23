using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace SmartStore.Core.Fakes
{
    public class FakeHttpContext : HttpContext
    {
        private readonly Dictionary<object, object> _items = new Dictionary<object, object>();
        private readonly IServiceProvider _serviceProvider;
        private readonly HttpRequest _request;
        private readonly HttpResponse _response;
        private readonly ISession _session;
        private readonly ClaimsPrincipal _user;

        public FakeHttpContext(IServiceProvider serviceProvider = null)
        {
            _serviceProvider = serviceProvider ?? new ServiceCollection().BuildServiceProvider();
            _request = new FakeHttpRequest(this);
            _response = new FakeHttpResponse(this);
            _session = new FakeHttpSession();
            _user = new ClaimsPrincipal(new ClaimsIdentity());
        }

        public override IFeatureCollection Features { get; } = new FeatureCollection();

        public override HttpRequest Request => _request;

        public override HttpResponse Response => _response;

        public override ConnectionInfo Connection { get; } = new FakeConnectionInfo();

        public override WebSocketManager WebSockets => throw new NotImplementedException();

        public override ClaimsPrincipal User
        {
            get => _user;
            set => throw new NotImplementedException();
        }

        public override IDictionary<object, object> Items
        {
            get => _items;
            set => throw new NotImplementedException();
        }

        public override IServiceProvider RequestServices
        {
            get => _serviceProvider;
            set => throw new NotImplementedException();
        }

        public override CancellationToken RequestAborted { get; set; }

        public override string TraceIdentifier { get; set; } = Guid.NewGuid().ToString();

        public override ISession Session
        {
            get => _session;
            set => throw new NotImplementedException();
        }

        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }

    public class FakeConnectionInfo : ConnectionInfo
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public override System.Net.IPAddress RemoteIpAddress { get; set; }
        public override int RemotePort { get; set; }
        public override System.Net.IPAddress LocalIpAddress { get; set; }
        public override int LocalPort { get; set; }
        public override System.Security.Cryptography.X509Certificates.X509Certificate2 ClientCertificate { get; set; }

        public override Task<System.Security.Cryptography.X509Certificates.X509Certificate2> GetClientCertificateAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ClientCertificate);
        }
    }
}
