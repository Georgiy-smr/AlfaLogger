using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AlfaLoggerLib.Logging;
using AlfaLoggerLib.Logging.Events.Base;

namespace AlfaLoggerLib.Logging
{
    public interface IAlfaLogger
    {
        Task Log(EventLogging eventLogging);
    }
}
