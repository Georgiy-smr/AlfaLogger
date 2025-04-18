using AlfaLoggerLib.Extension;
using AlfaLoggerLib.Logging;
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
    }
}