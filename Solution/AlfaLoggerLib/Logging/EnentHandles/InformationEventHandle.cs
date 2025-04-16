using AlfaLoggerLib.Logging.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlfaLoggerLib.Logging.EnentHandles;

internal class InformationEventHandle : INotificationHandler<InformationEvent>
{
    private readonly ILogger<InformationEventHandle> _logger;

    public InformationEventHandle(ILogger<InformationEventHandle> logger)
    {
        _logger = logger;
    }
    public Task Handle(InformationEvent notification, CancellationToken cancellationToken)
    {
        string formattedDate = notification.TimeEvent.ToString("dd-MM-yyyy HH:mm:ss");
        _logger.LogInformation("{1} {2} {3}",
            formattedDate,
            notification.EventPublishName,
            notification.InformationMessage);
        return Task.CompletedTask;
    }
}