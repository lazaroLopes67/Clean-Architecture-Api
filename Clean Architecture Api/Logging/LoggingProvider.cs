using System.Collections.Concurrent;

namespace Criando_Minha_Primeira_API.Logging
{
    /// <summary>
    /// Custom logger provider responsible for creating and managing logger instances.
    /// Implements ILoggerProvider to integrate with the ASP.NET Core logging system.
    /// </summary>
    public class LoggingProvider : ILoggerProvider
    {
        private readonly IConfiguration _configuration;
        public LoggingProvider(IConfiguration configuration) => _configuration = configuration;
        /// <summary>
        /// Thread-safe collection used to store and reuse logger instances
        /// based on their category name.
        /// </summary>
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new();
        /// <summary>
        /// Creates or retrieves an existing logger associated with the specified category.
        /// If a logger for the category already exists, it is reused.
        /// Otherwise, a new CustomLogger instance is created and stored.
        /// </summary>
        /// <param name="categoryName">
        /// The category name associated with the logger.
        /// Usually represents the class or component generating logs.
        /// </param>
        /// <returns>
        /// An ILogger instance associated with the specified category.
        /// </returns>
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new CustomLogger(name, _configuration));
        }
        /// <summary>
        /// Releases resources used by the logging provider.
        /// Clears all cached logger instances from memory.
        /// </summary>
        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}