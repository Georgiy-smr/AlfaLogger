using System.Linq.Expressions;
using AlfaLoggerLib.Extension;
using AlfaLoggerLib.Logging;
using AlfaLoggerLib.Logging.Events;
using Data.Entities;
using Data.Enums;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Repository;

namespace RepositoryTests
{
    public class ItemsTests
    {
        
        [Fact]
        public async void GetItemsInformation()
        {
            //Arrange 

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

            var sut = serviceProvider.GetRequiredService<LogsRepository>();

            //Act

            var result = await sut.Events(
                size:1,
                zeroStart:0,
                filters:
                new List<Expression<Func<Log, bool>>>()
                {
                    x => x.Message == testMessage,
                    x => x.TypeEvent == TypeEvent.Information
                });

            //Assert

            Assert.NotNull(result.Result.Single());
        }


        [Fact]
        public async void GetItemsErrorEvent()
        {
            //Arrange 

            ServiceCollection services = new ServiceCollection();
            services.AddAlfaLogger("appDataBase.db");
            services.AddRepositoryLogs();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var init = serviceProvider.GetRequiredService<LoggerInitialization>();
            if (!await init.InitializeAsync())
                throw new Exception();
            var logger = serviceProvider.GetRequiredService<IAlfaLogger>();

            string testMessage = Guid.NewGuid().ToString();
            await logger.Log(new ErrorEvent(DateTime.Now, nameof(ItemsTests))
            {
                ErrorMessage = testMessage
            });

            var sut = serviceProvider.GetRequiredService<LogsRepository>();

            //Act

            var result = await sut.Events(
                size: 1,
                zeroStart: 0,
                filters:
                new List<Expression<Func<Log, bool>>>()
                {
                    x => x.Message == testMessage,
                    x => x.TypeEvent == TypeEvent.Error
                });

            //Assert

            Assert.NotNull(result.Result.Single());
        }


        [Fact]
        public async void GetItemsExteptions()
        {
            //Arrange 

            ServiceCollection services = new ServiceCollection();
            services.AddAlfaLogger("appDataBase.db");
            services.AddRepositoryLogs();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var init = serviceProvider.GetRequiredService<LoggerInitialization>();
            if (!await init.InitializeAsync())
                throw new Exception();
            var logger = serviceProvider.GetRequiredService<IAlfaLogger>();

            string testMessage = Guid.NewGuid().ToString();

            var testService = new TestService();

    

            var sut = serviceProvider.GetRequiredService<LogsRepository>();


            //Act
            try
            {
                testService.GoTestWork(testMessage);
            }
            catch (Exception e)
            {
                await logger.Log(new ExceptionEvent(DateTime.Now, nameof(ItemsTests))
                {
                    Exception = e,
                });
            }

            var result = await sut.Events(
                size: 1,
                zeroStart: 0,
                filters:
                new List<Expression<Func<Log, bool>>>()
                {
                    x => x.Message.Contains(testMessage),
                    x => x.TypeEvent == TypeEvent.Critical
                });


            //Assert
            Assert.True(result.Result.Any(x => x.Message.Contains(testMessage)));
        }
    }
}