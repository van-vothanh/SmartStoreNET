using System;
// TODO: .NET 8 - Replace NuGet.Core with NuGet.Protocol
using SmartStore.Core.Logging;
using Log = SmartStore.Core.Logging;

// TODO: .NET 8 Migration - NuGet.Core not compatible, needs replacement with NuGet.Protocol
#if FALSE_NUGET_CORE_NOT_SUPPORTED
namespace SmartStore.Core.Packaging
{
    internal class NugetLogger : NuGet.ILogger
    {
        private readonly Log.ILogger _logger;

        public NugetLogger(Log.ILogger logger)
        {
            _logger = logger;
        }

        public void Log(MessageLevel level, string message, params object[] args)
        {
            switch (level)
            {
                case MessageLevel.Debug:
                    _logger.Debug(String.Format(message, args));
                    break;
                case MessageLevel.Error:
                    _logger.Error(String.Format(message, args));
                    break;
                case MessageLevel.Info:
                    _logger.Info(String.Format(message, args));
                    break;
                case MessageLevel.Warning:
                    _logger.Warn(String.Format(message, args));
                    break;
            }
        }

        public FileConflictResolution ResolveFileConflict(string message)
        {
            return FileConflictResolution.OverwriteAll;
        }
    }
}
#endif
