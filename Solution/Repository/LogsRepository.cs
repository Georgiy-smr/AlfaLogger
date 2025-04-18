using System.Linq.Expressions;
using Data.Entities;
using Data.Entities.Base;
using Data.Enums;
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
        public Task<IStatusGeneric<IEnumerable<LoggingEventDto>>> Events(
            List<Expression<Func<Log,bool>>>? filters = null,
            List<Expression<Func<Log, object>>>? includes = null,
            CancellationToken token = default)
        {
            return _mediator.Send(new GetEventsCommand()
            {
                Filters = filters,
                Includes = includes
            }, token);
        }
    }
}
