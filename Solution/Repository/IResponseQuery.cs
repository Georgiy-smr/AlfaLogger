using MediatR;
using Repository.Commands;
using Repository.DtoObjects;
using StatusGeneric;

namespace Repository;

public interface IResponseQuery<in TCommandQuery, TDtoObject>
{
    Task<TDtoObject> GetAsync(TCommandQuery query, CancellationToken token);
}

public class DataService : IResponseQuery<GetEventsCommand, IStatusGeneric<LoggingEventDto>>
{
    private readonly IMediator _mediator;

    public DataService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public Task<IStatusGeneric<LoggingEventDto>> GetAsync(GetEventsCommand query, CancellationToken token)
    {
        return _mediator.Send(query, token);
    }
}