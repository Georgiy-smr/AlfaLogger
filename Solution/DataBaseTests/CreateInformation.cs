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
            //Arrange

            ServiceCollection services = new ServiceCollection();
            services.AddAlfaLogger("appDataBase.db");
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var init = serviceProvider.GetRequiredService<LoggerInitialization>();
            if (!await init.InitializeAsync())
                throw new Exception();
            var sut = serviceProvider.GetRequiredService<IAlfaLogger>();
            string testMessage = Guid.NewGuid().ToString();

            //Act

            await sut.Log(new InformationEvent(DateTime.Now, nameof(CreateInformation))
            {
                InformationMessage = testMessage
            });
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var any = await context.Logs.AsQueryable().AnyAsync(x => x.Message.Equals(testMessage));

            //Assert

            Assert.True(any);
        }
    }
}