﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Repository.Commands;
using Repository.DtoObjects;
using Repository.Services;
using StatusGeneric;

namespace Repository
{
    public static class Registrar
    {
        public static IServiceCollection AddRepositoryLogs(this IServiceCollection collection)
        {
            return collection


                    .AddScoped(typeof(IGetItems<>), typeof(Items<>))

                    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                    .AddScoped<LogsRepository>()
                ;
        }
    }
}
