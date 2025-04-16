using AlfaLoggerLib.Logging.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlfaLoggerLib.Logging.EnentHandles;

internal class ExceptionEventHandle : INotificationHandler<ExceptionEvent>
{
    private readonly ILogger<ExceptionEventHandle> _logger;

    public ExceptionEventHandle(ILogger<ExceptionEventHandle> logger)
    {
        _logger = logger;
    }
    public Task Handle(ExceptionEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{1} {2} {3},{4}",
            notification.TimeEvent,
            notification.EventPublishName,
            notification.Exception,
            notification.Exception.Message);
        return Task.CompletedTask;
    }
}