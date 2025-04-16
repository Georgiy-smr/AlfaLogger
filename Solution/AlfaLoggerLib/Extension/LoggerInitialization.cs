using ContextEf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlfaLoggerLib.Extension
{
    public class LoggerInitialization
    {
        private readonly InitializationDataBase _dataBase;

        public LoggerInitialization(InitializationDataBase dataBase)
        {
            _dataBase = dataBase;
        }
        public async Task<bool> InitializeAsync(CancellationToken token = default)
        {
            return await _dataBase.InitializeAsync(token);
        }
    }
}
