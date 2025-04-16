using AlfaLoggerLib.Logging.Events;
using ContextEf;
using Data.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlfaLoggerLib.Logging.EnentHandles;

internal class InformationEventHandle : INotificationHandler<InformationEvent>
{
    private readonly ILogger<InformationEventHandle> _logger;
    private readonly AppDbContext _context;

    public InformationEventHandle(
        ILogger<InformationEventHandle> logger,
        AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public async Task Handle(InformationEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Logs.AddAsync(new()
            {
                Date = notification.TimeEvent,
                EventPublishName = notification.EventPublishName,
                TypeEvent = TypeEvent.Information,
                Message = notification.InformationMessage
            }, cancellationToken).ConfigureAwait(false);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError("{1} {2}",
                e,
                e.Message);
        }
    }
}