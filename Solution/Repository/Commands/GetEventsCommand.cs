using System.Linq.Expressions;
using Data.Entities;
using Data.Entities.Base;
using MediatR;
using Repository.DtoObjects;
using StatusGeneric;

namespace Repository.Commands;


internal abstract record RepositoryCommand<T>() where T : Entity, new()
{
    public required IEnumerable<Expression<Func<T, bool>>>? Filters { get; init; } 
    public required IEnumerable<Expression<Func<T, object>>>? Includes { get; init; }
    public required int Size { get; init; }
    public required int ZeroStart { get; init; }

}

internal record GetEventsCommand() : 
    RepositoryCommand<Log>,
    IRequest<IStatusGeneric<IEnumerable<LoggingEventDto>>>;