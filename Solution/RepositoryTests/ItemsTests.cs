using AlfaLoggerLib.Extension;
using AlfaLoggerLib.Logging;
using AlfaLoggerLib.Logging.Events;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace RepositoryTests
{
    public class ItemsTests
    {
        
        [Fact]
        public async void GetItems()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAlfaLogger("appDataBase.db");
            services.AddRepositoryLogs();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var init = serviceProvider.GetRequiredService<LoggerInitialization>();
            if (!await init.InitializeAsync())
                throw new Exception();
            var logger = serviceProvider.GetRequiredService<IAlfaLogger>();


            string testMessage = Guid.NewGuid().ToString();
            await logger.Log(new InformationEvent(DateTime.Now, nameof(ItemsTests))
            {
                InformationMessage = testMessage
            });


            var repository = serviceProvider.GetRequiredService<LogsRepository>();


            var result = await repository.Events();

            Assert.True(result.Result.Any());

        }
    }
}