// TODO: .NET 8 Migration - Fake classes need rewrite for ASP.NET Core
#if FALSE_FAKE_CLASSES_NOT_COMPATIBLE
﻿using Microsoft.AspNetCore.Mvc;

namespace SmartStore.Core.Fakes
{
    public class FakeController : Controller
    {
    }
}
#endif
