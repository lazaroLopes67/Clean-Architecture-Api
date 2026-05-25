namespace Criando_Minha_Primeira_API.Logging
{
    /// <summary>
    /// Custom logger implementation responsible for writing log messages
    /// into a text file.
    /// Integrates with the ASP.NET Core logging infrastructure
    /// through the ILogger interface.
    /// </summary>
    public class CustomLogger : ILogger
    {
        /// <summary>
        /// Name of the logger category.
        /// Usually represents the class or component generating the logs.
        /// </summary>
        private readonly string _loggerName;

        /// <summary>
        /// Provides access to application configuration values,
        /// such as the log file path defined in appsettings.json.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the CustomLogger class.
        /// </summary>
        /// <param name="loggerName">
        /// The category name associated with the logger.
        /// </param>
        /// <param name="configuration">
        /// Application configuration instance used to retrieve settings.
        /// </param>
        public CustomLogger(string loggerName, IConfiguration configuration)
        {
            _loggerName = loggerName;
            _configuration = configuration;
        }

        /// <summary>
        /// Begins a logical logging scope.
        /// This logger does not implement scopes,
        /// so the method returns null.
        /// </summary>
        /// <typeparam name="TState">The type of the scope state.</typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>Null because scopes are not implemented.</returns>
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        /// <summary>
        /// Determines whether the specified log level is enabled.
        /// Only Information level and higher are allowed.
        /// </summary>
        /// <param name="logLevel">The log severity level.</param>
        /// <returns>
        /// True if the log level is Information or higher; otherwise, false.
        /// </returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// Writes a log entry into the configured log file.
        /// </summary>
        /// <typeparam name="TState">The type of the object being logged.</typeparam>
        /// <param name="logLevel">The severity level of the log entry.</param>
        /// <param name="eventId">Identifier for the log event.</param>
        /// <param name="state">The log message or object state.</param>
        /// <param name="exception">Associated exception, if any.</param>
        /// <param name="formatter">
        /// Function responsible for formatting the log message.
        /// </param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            // Retrieves the log file path from application configuration
            string? path = _configuration["LoggingPath"];

            // Ensures the path exists before attempting to write logs
            if (!string.IsNullOrWhiteSpace(path))
            {
                // Formats the log message
                string content = $"logger name: {_loggerName} - {formatter(state, exception)}";
                // Writes the formatted log into the file
                WriteInFile(path, content);
            }
        }

        /// <summary>
        /// Writes content into the specified file.
        /// Uses append mode to preserve existing log entries.
        /// </summary>
        /// <param name="path">The file path where logs will be written.</param>
        /// <param name="content">The log content to write.</param>
        public void WriteInFile(string path, string content)
        {
            // Creates a StreamWriter in append mode
            using StreamWriter sw = new(path, true);
            sw.WriteLine(content);            
        }
    }
}