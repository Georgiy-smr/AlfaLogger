using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AlfaLoggerLib.Logging;
using ContextEf;
using Data.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Commands;
using Repository.DtoObjects;
using Serilog;
using StatusGeneric;
using Log = Serilog.Log;

namespace AlfaLoggerLib.Extension
{
    public static class AlfaLoggerRegistration
    {
        public static IServiceCollection AddAlfaLogger(this IServiceCollection collection, string path)
        {
            return collection
                .RegistrarDataBase(path)
                .AddSingleton<LoggerInitialization>()
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddScoped<IAlfaLogger, AlfaLogger>()
               ;
        }

    }
}
