using Microsoft.Extensions.DependencyInjection;
using Repository.Commands;
using Repository.DtoObjects;
using StatusGeneric;

namespace Repository
{
    public class LogsRepository
    {
        private readonly IServiceProvider _provider;
        private readonly DataService _service;

        public LogsRepository(
            IServiceProvider provider,
            DataService service
            )
        {
            _provider = provider;
            _service = service;
        }
        public async Task<IStatusGeneric<LoggingEventDto>> Events(CancellationToken token = default)
        {
            await using var scope = _provider.CreateAsyncScope();

            DataService service = scope.ServiceProvider.GetRequiredService<DataService>();

            var result = await _service.GetAsync(new GetEventsCommand(), token);
            return result;
        }
    }
}
