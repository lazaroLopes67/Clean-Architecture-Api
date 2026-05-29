namespace Criando_Minha_Primeira_API.Pagination
{
    /// <summary>
    /// Represents the query parameters used for pagination.
    /// 
    /// This class is commonly used to receive pagination values
    /// from the request URL, such as:
    /// 
    /// ?PageNumber=1&PageSize=10
    /// 
    /// It also contains validation rules and default values
    /// to ensure safe and consistent pagination behavior.
    /// </summary>
    public class QueryParams
    {
        /// <summary>
        /// Defines the maximum number of records
        /// allowed per page.
        /// 
        /// This limit helps protect the API
        /// against excessively large requests.
        /// </summary>
        const int MaxPageSize = 50;

        /// <summary>
        /// Backing field used to store the current page number.
        /// 
        /// Initialized with page 1 as the default value.
        /// </summary>
        private int _pageNumber = 1;

        /// <summary>
        /// Represents the current page number requested by the client.
        /// 
        /// If the provided value is less than or equal to zero,
        /// the page number is automatically set to 1.
        /// </summary>
        public int PageNumber
        {
            get { return _pageNumber; }

            set
            {
                // Ensures the page number is always greater than zero
                _pageNumber = (value > 0) ? value : 1;
            }
        }

        /// <summary>
        /// Backing field used to store the page size value.
        /// 
        /// Initialized with 10 items per page by default.
        /// </summary>
        private int _pageSize = 10;

        /// <summary>
        /// Represents the number of records returned per page.
        /// 
        /// Validation rules:
        /// - Values less than or equal to zero are replaced with 10.
        /// - Values greater than MaxPageSize are limited to MaxPageSize.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;

            set
            {
                // Applies validation rules to prevent
                // invalid or excessively large page sizes
                _pageSize = (value <= 0)
                    ? 10
                    : (value > MaxPageSize
                        ? MaxPageSize
                        : value);
            }
        }
    }
}