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
    }
}