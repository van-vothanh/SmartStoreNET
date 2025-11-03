#if false // Disabled for .NET 8 - NuGet.Core is obsolete
﻿namespace SmartStore.Core.Packaging
{
    public class PackageInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public ExtensionDescriptor ExtensionDescriptor { get; set; }
    }
}
#endif
