using System;
// TODO: Migrate to Microsoft.Extensions.Logging
// using NuGet;
using SmartStore.Core.Logging;

namespace SmartStore.Core.Packaging
{
    // TODO: Migrate to Microsoft.Extensions.Logging
    // NuGet.Core ILogger is obsolete
    /*
    public class NugetLogger : NuGet.ILogger
    {
        private readonly ILogger _logger;

        public NugetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Log(MessageLevel level, string message, params object[] args)
        {
            switch (level)
            {
                case MessageLevel.Debug:
                    _logger.Debug(message, args);
                    break;
                case MessageLevel.Info:
                    _logger.Info(message, args);
                    break;
                case MessageLevel.Warning:
                    _logger.Warn(message, args);
                    break;
                case MessageLevel.Error:
                    _logger.Error(message, args);
                    break;
            }
        }

        public FileConflictResolution ResolveFileConflict(string message)
        {
            return FileConflictResolution.IgnoreAll;
        }
    }
    */
}
