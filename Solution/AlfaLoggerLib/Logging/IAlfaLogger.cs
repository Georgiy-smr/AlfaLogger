using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AlfaLoggerLib.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlfaLoggerLib.Logging
{
    public interface IAlfaLogger
    {
        Task Log(EventLogging eventLogging);
    }

    internal class AlfaLogger : IAlfaLogger
    {
        private readonly IMediator _mediator;

        public AlfaLogger(IMediator mediator) => _mediator = mediator;

        public Task Log(EventLogging eventLogging) => _mediator.Publish(eventLogging);
    }

    public abstract record EventLogging(DateTime TimeEvent, string EventPublishName) : INotification;

    public record InformationEvent(DateTime TimeEvent, string EventPublishName) : EventLogging(
        TimeEvent, EventPublishName)
    {
        public required string InformationMessage { get; set; }
    }

    public record ErrorEvent(DateTime TimeEvent, string EventPublishName, string Message) : EventLogging(
        TimeEvent, EventPublishName)
    {
        public required string ErrorMessage { get; set; }
    }

    public  record ExceptionEvent(DateTime TimeEvent, string EventPublishName) : EventLogging(TimeEvent, EventPublishName)
    {
        public required Exception @Exception { get; set; }
    }

    internal class InformationEventHandle : INotificationHandler<InformationEvent>
    {
        private readonly ILogger<InformationEventHandle> _logger;

        public InformationEventHandle(ILogger<InformationEventHandle> logger)
        {
            _logger = logger;
        }
        public Task Handle(InformationEvent notification, CancellationToken cancellationToken)
        {
            string formattedDate = notification.TimeEvent.ToString("dd-MM-yyyy HH:mm:ss");
            _logger.LogInformation("{1} {2} {3}",
                formattedDate,
                notification.EventPublishName,
                notification.InformationMessage);
            return Task.CompletedTask;
        }
    }

    internal class ErrorEventHandle : INotificationHandler<ErrorEvent>
    {
        private readonly ILogger<ErrorEventHandle> _logger;

        public ErrorEventHandle(ILogger<ErrorEventHandle> logger)
        {
            _logger = logger;
        }
        public Task Handle(ErrorEvent notification, CancellationToken cancellationToken)
        {

            string formattedDate = notification.TimeEvent.ToString("dd-MM-yyyy HH:mm:ss");

            _logger.LogInformation("{1} {2} {3}",
                formattedDate,
                notification.EventPublishName,
                notification.ErrorMessage);
            return Task.CompletedTask;
        }
    }

    internal class ExceptionEventHandle : INotificationHandler<ExceptionEvent>
    {
        private readonly ILogger<ExceptionEventHandle> _logger;

        public ExceptionEventHandle(ILogger<ExceptionEventHandle> logger)
        {
            _logger = logger;
        }
        public Task Handle(ExceptionEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{1} {2} {3},{4}",
                notification.TimeEvent,
                notification.EventPublishName,
                notification.Exception,
                notification.Exception.Message);
            return Task.CompletedTask;
        }
    }

}
