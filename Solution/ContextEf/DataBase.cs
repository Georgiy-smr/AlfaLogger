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
            string pathFileDataBase)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(pathFileDataBase, nameof(pathFileDataBase));
            return collection
                .AddDbContextPool<AppDbContext>((servicesProvider, options) =>
                {
                    options.UseSqlite($"Data Source={pathFileDataBase}");
                })
                .AddScoped<InitializationDataBase>();
        }
    }
}
