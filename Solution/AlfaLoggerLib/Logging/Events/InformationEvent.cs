using AlfaLoggerLib.Logging.Events.Base;

namespace AlfaLoggerLib.Logging.Events;

public record InformationEvent(DateTime TimeEvent, string EventPublishName) : EventLogging(
    TimeEvent, EventPublishName)
{
    public required string InformationMessage { get; set; }
}