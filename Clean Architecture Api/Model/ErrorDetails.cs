namespace Criando_Minha_Primeira_API.Model
{
    /// <summary>
    /// Represents a standardized error response returned by the API.
    /// This class is used to structure error information in a consistent format
    /// for clients consuming the API.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// HTTP status code associated with the error.
        /// Default value is 500 (Internal Server Error).
        /// </summary>
        public int StatusCode { get; set; } = 500;
        /// <summary>
        /// Human-readable message describing the error.
        /// This message is intended to help the client understand what went wrong.
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// Detailed technical information about the error (stack trace).
        /// This should typically only be exposed in development environments
        /// for debugging purposes.
        /// </summary>
        public string? Trace { get; set; }
    }
}
