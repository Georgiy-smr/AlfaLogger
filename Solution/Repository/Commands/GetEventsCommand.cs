using Data.Entities;
using MediatR;
using Repository.DtoObjects;
using StatusGeneric;

namespace Repository.Commands;

public record GetEventsCommand() : 
    IRequest<IStatusGeneric<IEnumerable<LoggingEventDto>>>;