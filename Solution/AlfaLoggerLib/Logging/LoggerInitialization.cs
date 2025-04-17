using ContextEf;

namespace AlfaLoggerLib.Logging
{
    public class LoggerInitialization
    {
        private readonly InitializationDataBase _dataBase;

        public LoggerInitialization(InitializationDataBase dataBase)
        {
            _dataBase = dataBase;
        }
        public  Task<bool> InitializeAsync(CancellationToken token = default)
        {
            return _dataBase.InitializeAsync(token);
        }
    }
}
