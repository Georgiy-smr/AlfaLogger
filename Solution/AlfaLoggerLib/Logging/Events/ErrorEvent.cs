using AlfaLoggerLib.Logging.Events.Base;

namespace AlfaLoggerLib.Logging.Events;

public record ErrorEvent(DateTime TimeEvent, string EventPublishName) : EventLogging(
    TimeEvent, EventPublishName)
{
    public required string ErrorMessage { get; set; }
}