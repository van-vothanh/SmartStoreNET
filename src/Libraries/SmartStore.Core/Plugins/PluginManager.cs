using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SmartStore.Core.Logging;

namespace SmartStore.Core.Plugins
{
    public partial class PluginManager
    {
        internal static ILogger Logger = NullLogger.Instance;
        internal static DirectoryInfo _shadowCopyDir;
        internal const string PluginsLocation = "~/Plugins";
        
        public static IEnumerable<PluginDescriptor> ReferencedPlugins => new List<PluginDescriptor>();
        
        public static void MarkPluginAsInstalled(string systemName) { }
        public static void MarkPluginAsUninstalled(string systemName) { }
        public static bool IsActivePluginAssembly(Assembly assembly) => false;
    }
}
