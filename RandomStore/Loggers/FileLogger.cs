namespace RandomStore.Application.Loggers
{
    public class FileLogger : ILogger, IDisposable
    {
        private readonly string filePath;
        private static object _lock = new object();

        public FileLogger(string path)
        {
            filePath = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose() { }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId,
                    TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            lock (_lock)
            {
                File.AppendAllText(filePath, GenerateDateString() + formatter(state, exception) + Environment.NewLine);
            }
        }

        private string GenerateDateString()
        {
            var dateNow = DateTime.Now;
            return $"{dateNow.ToShortDateString()} {dateNow.ToShortTimeString()}: ";
        }
    }
}
