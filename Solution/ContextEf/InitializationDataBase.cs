using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextEf
{
    public class InitializationDataBase
    {
        private AppDbContext _db;
        private ILogger<InitializationDataBase> _Logger;
        public InitializationDataBase
            (AppDbContext db, ILogger<InitializationDataBase> Logger)
        {
            _db = db; _Logger = Logger;
        }

        public async Task<bool> InitializeAsync(CancellationToken token = default)
        {
            Stopwatch timer = Stopwatch.StartNew();
            try
            {
                await _db.Database.EnsureCreatedAsync(token);
                _Logger.LogInformation($"БД создана за {timer.Elapsed.TotalSeconds}");
            }
            catch (Exception e)
            {
                _Logger.LogInformation($"БД НЕ Создана");
                _Logger.LogInformation($"Ошибка типа {e}. Контекст ошибки {e.Message}");
                return false;
            }
            finally
            {
                timer.Stop();
            }
            return true;
        }
    }
}
