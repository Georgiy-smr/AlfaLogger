using System.Linq.Expressions;
using Data;
using Data.Entities;
using Data.Entities.Base;
using MediatR;
using Repository.DtoObjects;
using StatusGeneric;

namespace Repository.Commands;


public abstract record RepositoryCommand<T>() where T : IEntity, new()
{
    public required IEnumerable<Expression<Func<T, bool>>>? Filters { get; init; } 
    public required IEnumerable<Expression<Func<T, object>>>? Includes { get; init; }
    public required int Size { get; init; }
    public required int ZeroStart { get; init; }

}

public record GetEventsCommand() : 
    RepositoryCommand<Log>,
    IRequest<IStatusGeneric<IEnumerable<LoggingEventDto>>>;