using System;
using AlfaLoggerLib.Extension;
using AlfaLoggerLib.Logging;
using AlfaLoggerLib.Logging.Events;
using ContextEf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataBaseTests
{
    public class CreateInformation
    {
        [Fact]
        public async void Test1()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAlfaLogger("appDataBase.db");
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var init = serviceProvider.GetRequiredService<LoggerInitialization>();
            if (!await init.InitializeAsync())
                throw new Exception();

            var logger = serviceProvider.GetRequiredService<IAlfaLogger>();


            string testMessage = Guid.NewGuid().ToString();
            await logger.Log(new InformationEvent(DateTime.Now, nameof(CreateInformation))
            {
                InformationMessage = testMessage
            });


            var context = serviceProvider.GetRequiredService<AppDbContext>();

            var log = await context.Logs.AsQueryable().FirstAsync(x => x.Message.Equals(testMessage));
        }
    }
}