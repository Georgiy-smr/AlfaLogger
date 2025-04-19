using AlfaLogger.Repository.Extensions;
using ContextEf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.DtoObjects;
using StatusGeneric;

namespace Repository.Commands;

internal class GetEventsCommandHandler :
    IRequestHandler<GetEventsCommand, IStatusGeneric<IEnumerable<LoggingEventDto>>>
{
    private readonly IServiceProvider _provider;

    public GetEventsCommandHandler(
        IServiceProvider provider)
    {
        _provider = provider;
    }
    public async Task<IStatusGeneric<IEnumerable<LoggingEventDto>>> Handle(
        GetEventsCommand request, CancellationToken cancellationToken)
    {
        var status = new StatusGenericHandler<IEnumerable<LoggingEventDto>>();
        IEnumerable<LoggingEventDto> result = null!;
        await using var scope = _provider.CreateAsyncScope();
        try
        {
            var query =
                scope.ServiceProvider
                    .GetRequiredService<AppDbContext>().Logs
                    .AsQueryable()
                    .AsNoTracking()
                    .ApplyFilters(request.Filters)
                    .ApplyInclude(request.Includes)
                    .OrderByDesc()
                    .Page(request.ZeroStart, request.Size);

            if (!await query.AnyAsync(cancellationToken: cancellationToken))
            {
                status.AddError("items is empty");
                return status;
            }

            var list = await query
                .Select(x =>
                    new
                    {
                        eventName = x.EventPublishName,
                        date = x.Date,
                        message = x.Message,
                        type = x.TypeEvent
                    }).ToListAsync(cancellationToken: cancellationToken);

            result = list
                .Select(x => new LoggingEventDto(
                    x.eventName,
                    x.message,
                    x.type,
                    x.date));
        }
        catch (Exception e)
        {
            status.AddError(e, e.Message);
        } 
        return status.SetResult(result);
    }
}