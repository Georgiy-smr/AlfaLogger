using AlfaLoggerLib.Logging.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlfaLoggerLib.Logging.EnentHandles;

internal class ErrorEventHandle : INotificationHandler<ErrorEvent>
{
    private readonly ILogger<ErrorEventHandle> _logger;

    public ErrorEventHandle(ILogger<ErrorEventHandle> logger)
    {
        _logger = logger;
    }
    public Task Handle(ErrorEvent notification, CancellationToken cancellationToken)
    {

        string formattedDate = notification.TimeEvent.ToString("dd-MM-yyyy HH:mm:ss");

        _logger.LogInformation("{1} {2} {3}",
            formattedDate,
            notification.EventPublishName,
            notification.ErrorMessage);
        return Task.CompletedTask;
    }
}