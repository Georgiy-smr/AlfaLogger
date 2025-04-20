using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContextEf
{
    public static  class DataBase
    {
        public static IServiceCollection RegistrarDataBase(
            this IServiceCollection collection,
            string? pathFileDataBase = null)
        {
            return collection
                .AddSingleton<DataBaseSettings>()
                .AddDbContextPool<AppDbContext>((servicesProvider, options) =>
                {
                    if(string.IsNullOrWhiteSpace(pathFileDataBase))
                        pathFileDataBase = servicesProvider.GetRequiredService<DataBaseSettings>().ToString();
                    options.UseSqlite($"Data Source={pathFileDataBase}");
                })
                .AddScoped<InitializationDataBase>();
        }
    }
}
