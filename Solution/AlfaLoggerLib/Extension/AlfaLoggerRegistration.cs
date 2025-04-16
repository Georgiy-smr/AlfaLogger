using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AlfaLoggerLib.Logging;
using ContextEf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace AlfaLoggerLib.Extension
{
    public static class AlfaLoggerRegistration
    {
        public static IServiceCollection AddAlfaLogger(this IServiceCollection collection, string path)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            return collection
                .RegistrarDataBase(path)
                .AddSingleton<LoggerInitialization>()
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddScoped<IAlfaLogger, AlfaLogger>()
                .AddLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddSerilog(Log.Logger);
                });
        }
    }
}
