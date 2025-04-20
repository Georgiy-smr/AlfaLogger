using System.Reflection;
using AlfaLoggerLib.Logging;
using ContextEf;
using Microsoft.Extensions.DependencyInjection;
using Implements = AlfaLoggerLib.Logging.Implements;

namespace AlfaLoggerLib.Extension
{
    public static class AlfaLoggerRegistration
    {
        public static IServiceCollection AddAlfaLogger(
            this IServiceCollection collection,
            string? path = null)
        {
            return collection
                    .AddLogging()
                    .RegistrarDataBase(path)
                    .AddSingleton<LoggerInitialization>()
                    .AddScoped<IAlfaLogger, Implements.AlfaLogger>()
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
               ;
        }

    }
}
