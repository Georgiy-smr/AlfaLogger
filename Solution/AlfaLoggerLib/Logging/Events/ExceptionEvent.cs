using AlfaLoggerLib.Logging.Events.Base;

namespace AlfaLoggerLib.Logging.Events;

public  record ExceptionEvent(DateTime TimeEvent, string EventPublishName) : EventLogging(TimeEvent, EventPublishName)
{
    public required Exception @Exception { get; set; }
}