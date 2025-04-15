using AlfaLoggerLib.Extension;
using AlfaLoggerLib.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AlfaLoggerTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAlfaLogger();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<IAlfaLogger>();

            logger.Log(new InformationEvent(DateTime.Now, "Test1")
            {
                InformationMessage = "HelloWorld!"
            });
        }
    }
}