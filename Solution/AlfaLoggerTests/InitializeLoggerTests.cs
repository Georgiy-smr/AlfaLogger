using AlfaLoggerLib.Extension;
using AlfaLoggerLib.Logging;
using AlfaLoggerLib.Logging.Events;
using Microsoft.Extensions.DependencyInjection;

namespace AlfaLoggerTests
{
    public class InitializeLoggerTests
    {
        [Fact]
        public async void TestInitialize()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAlfaLogger("appDataBase.db");
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var init = serviceProvider.GetRequiredService<LoggerInitialization>();
            var result = await init.InitializeAsync();
            Assert.True(result);
        }


        [Fact]
        public async void Test()
        {
            //Arrange 

            ServiceCollection services = new ServiceCollection();
            services.AddAlfaLogger("bigDataBase.db");
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var init = serviceProvider.GetRequiredService<LoggerInitialization>();
            if (!await init.InitializeAsync())
                throw new Exception();
            var logger = serviceProvider.GetRequiredService<IAlfaLogger>();

            for (int i = 0; i < 1000; i++)
            {
                string testMessage = Guid.NewGuid().ToString();
                await logger.Log(new InformationEvent(DateTime.Now, "APC:" + nameof(InitializeLoggerTests))
                {
                    InformationMessage = testMessage
                });
            }
        }
    }
}