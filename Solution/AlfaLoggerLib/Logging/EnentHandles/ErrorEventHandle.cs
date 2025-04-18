using AlfaLoggerLib.Logging.Events;
using ContextEf;
using Data.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlfaLoggerLib.Logging.EnentHandles;

internal class ErrorEventHandle : INotificationHandler<ErrorEvent>
{
    private readonly ILogger<ErrorEventHandle> _logger;
    private readonly AppDbContext _context;

    public ErrorEventHandle(
        ILogger<ErrorEventHandle> logger,
        AppDbContext context
        )
    {
        _logger = logger;
        _context = context;
    }
    public async Task Handle(ErrorEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Logs.AddAsync(new()
            {
                Date = notification.TimeEvent,
                EventPublishName = notification.EventPublishName,
                TypeEvent = TypeEvent.Error,
                Message = notification.ErrorMessage
            }, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError("{1} {2} {3}",
                nameof(ErrorEventHandle),
                e,
                e.Message);
        }
    }
}