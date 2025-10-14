// TODO: Migrate to ASP.NET Core - Fake classes need complete rewrite
// Use DefaultHttpContext and related ASP.NET Core test utilities instead
/*
﻿using System.Linq;
using System.Security.Principal;

namespace SmartStore.Core.Fakes
{
    public class FakePrincipal : IPrincipal
    {
        private readonly IIdentity _identity;
        private readonly string[] _roles;

        public FakePrincipal(IIdentity identity, string[] roles)
        {
            _identity = identity;
            _roles = roles;
        }


        public IIdentity Identity => _identity;

        public bool IsInRole(string role)
        {
            return _roles != null && _roles.Contains(role);
        }
    }
}*/
