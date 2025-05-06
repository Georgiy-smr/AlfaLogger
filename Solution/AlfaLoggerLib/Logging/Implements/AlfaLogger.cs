using AlfaLoggerLib.Logging.Events.Base;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlfaLoggerLib.Logging.Implements;

internal class AlfaLogger : IAlfaLogger
{
    private readonly ILogger<AlfaLogger> _logger;
    private readonly IServiceProvider _serviceProvider;
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    public AlfaLogger(
        ILogger<AlfaLogger> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task Log(EventLogging eventLogging)
    {
        try
        {
            await _semaphore.WaitAsync();
            await using var scope = _serviceProvider.CreateAsyncScope();
            await scope.ServiceProvider.GetRequiredService<IMediator>().Publish(eventLogging);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }
        finally 
        {
            _semaphore.Release();
        }
    }
}