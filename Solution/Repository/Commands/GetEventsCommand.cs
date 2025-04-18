using Data.Entities;
using MediatR;
using Repository.DtoObjects;
using StatusGeneric;

namespace Repository.Commands;

internal record GetEventsCommand() : 
    IRequest<IStatusGeneric<IEnumerable<LoggingEventDto>>>;