using System.Security.Cryptography;
using AlfaLoggerLib.Extension;
using AlfaLoggerLib.Logging;
using AlfaLoggerLib.Logging.Events;
using ContextEf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AlfaLoggerTests
{
    public class InitializeLoggerTests
    {
        [Fact]
        public async void TestInitialize()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAlfaLogger("C:\\Users\\Гоша и Аня\\AlfaLogger\\Solution\\AlfaLoggerTests\\TestDataBase\\appDataBase.db");
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var init = serviceProvider.GetRequiredService<LoggerInitialization>();
            var result = await init.InitializeAsync();
            Assert.True(result);
        }


      
    }
}