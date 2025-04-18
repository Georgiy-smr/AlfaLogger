using System.Security.Cryptography;
using AlfaLoggerLib.Extension;
using AlfaLoggerLib.Logging;
using AlfaLoggerLib.Logging.Events;
using ContextEf;
using Data.Entities;
using DataBaseTests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Commands;

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
        public async void Test1()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAlfaLogger("appDataBase.db");
            services.AddRepositoryLogs();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var init = serviceProvider.GetRequiredService<LoggerInitialization>();
            if (!await init.InitializeAsync())
                throw new Exception();

            var repository = serviceProvider.GetRequiredService<LogsRepository>();


            var result = await repository.Events();

        }
        [Fact]
        public async void Test2()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAlfaLogger("appDataBase.db");
            services.AddRepositoryLogs();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var init = serviceProvider.GetRequiredService<LoggerInitialization>();
            if (!await init.InitializeAsync())
                throw new Exception();


            var service = serviceProvider.GetRequiredService<ServiceTestMediator>();

            var s = await service.Go();
            
        }

    }
}