using MediatR;

namespace AlfaLoggerLib.Logging.Events.Base;

public abstract record EventLogging(DateTime TimeEvent, string EventPublishName) : INotification;