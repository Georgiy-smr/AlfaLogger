using AlfaLoggerLib.Logging.Events.Base;
using MediatR;

namespace AlfaLoggerLib.Logging.Implements;

internal class AlfaLogger : IAlfaLogger
{
    private readonly IMediator _mediator;

    public AlfaLogger(IMediator mediator) => _mediator = mediator;

    public Task Log(EventLogging eventLogging) => _mediator.Publish(eventLogging);
}