using ContextEf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.DtoObjects;
using StatusGeneric;

namespace Repository.Commands;

public class GetEventsCommandHandler :
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
        try
        {
            await using var scope = _provider.CreateAsyncScope();
            var service = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var query = service.Logs.AsQueryable();

            if (!await query.AnyAsync(cancellationToken: cancellationToken))
                status.AddError("items is empty");

            var list = await query
                .Select(x =>
                    new
                    {
                        eventName = x.EventPublishName,
                        date = x.Date,
                        message = x.Message,
                    }).ToListAsync(cancellationToken: cancellationToken);

            result = list
                .Select(x => new LoggingEventDto(x.message));
        }
        catch (Exception e)
        {
            status.AddError(e, e.Message);
        } 
        return status.SetResult(result);
    }
}