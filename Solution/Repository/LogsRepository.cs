using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Repository.Commands;
using Repository.DtoObjects;
using StatusGeneric;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository
{
    public class LogsRepository
    {
        private readonly IMediator _mediator;
        public LogsRepository(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task<IStatusGeneric<IEnumerable<LoggingEventDto>>> Events(CancellationToken token = default)
        {
            return _mediator.Send(new GetEventsCommand(), token);
        }
    }
}
