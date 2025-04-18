using System.Reflection;
using AlfaLoggerLib.Logging;
using ContextEf;
using Microsoft.Extensions.DependencyInjection;

namespace AlfaLoggerLib.Extension
{
    public static class AlfaLoggerRegistration
    {
        public static IServiceCollection AddAlfaLogger(this IServiceCollection collection, string path)
        {
            return collection
                    .AddLogging()
                    .RegistrarDataBase(path)
                    .AddSingleton<LoggerInitialization>()
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                    .AddScoped<IAlfaLogger, AlfaLogger>()
               ;
        }

    }
}
