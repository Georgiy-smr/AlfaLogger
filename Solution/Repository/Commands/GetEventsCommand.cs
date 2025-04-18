using ContextEf;
using Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.DtoObjects;
using StatusGeneric;

namespace Repository.Commands;

public record GetEventsCommand() : 
    IRequest<IStatusGeneric<LoggingEventDto>>;

public class GetEventsCommandHandler :
    IRequestHandler<GetEventsCommand, IStatusGeneric<LoggingEventDto>>
{
    private readonly IServiceProvider _provider;

    public GetEventsCommandHandler(
        IServiceProvider provider)
    {
        _provider = provider;
    }
    public async Task<IStatusGeneric<LoggingEventDto>> Handle(
        GetEventsCommand request, CancellationToken cancellationToken)
    {
        await using var scope = _provider.CreateAsyncScope();
        var service = scope.ServiceProvider.GetRequiredService<AppDbContext>();


        var status = new StatusGenericHandler<LoggingEventDto>();
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

        var result = list
            .Select(x => new LoggingEventDto(x.message)).First();

        return status.SetResult(result);
    }
}