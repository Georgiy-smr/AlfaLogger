using AlfaLoggerLib.Logging.Events;
using ContextEf;
using Data.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlfaLoggerLib.Logging.EnentHandles;

internal class ExceptionEventHandle : INotificationHandler<ExceptionEvent>
{
    private readonly ILogger<ExceptionEventHandle> _logger;
    private readonly AppDbContext _context;

    public ExceptionEventHandle(
        ILogger<ExceptionEventHandle> logger,
        AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public async Task Handle(ExceptionEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Logs.AddAsync(new()
            {
                Date = notification.TimeEvent,
                EventPublishName = notification.Exception.ToString(),
                TypeEvent = TypeEvent.Critical,
                Message = notification.Exception.Message,
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