using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging;

namespace ProjectRimFactory.Logging
{
    public static class LogHelper
    {
        private static volatile ILogger _logger;
        private static readonly object _locker = new object();
        public static ILogger GetLogger()
        {
            if (_logger != null) return _logger;
            lock (_locker)
            {
                if (_logger != null) return _logger;
                _logger = CompositeLogger.Create(new VerseLogger());
            }

            return _logger;
        }
    }
}
