namespace Criando_Minha_Primeira_API.Pagination
{
    /// <summary>
    /// Generic pagination class responsible for storing
    /// paginated data and pagination metadata.
    /// 
    /// This class inherits from List<T>, allowing it
    /// to behave like a standard collection while also
    /// containing additional pagination information.
    /// </summary>
    /// <typeparam name="T">
    /// Represents the entity type being paginated.
    /// </typeparam>
    public class PagedList<T> : List<T> where T : class
    {
        /// <summary>
        /// Total number of records available in the data source.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Current page number being returned.
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Total number of available pages
        /// based on the total number of records.
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Number of records displayed per page.
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Indicates whether there is a previous page available.
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;

        /// <summary>
        /// Indicates whether there is a next page available.
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;

        /// <summary>
        /// Initializes a new instance of the PagedList class.
        /// 
        /// Receives the paginated items and pagination metadata.
        /// </summary>
        /// <param name="items">
        /// Collection of items belonging to the current page.
        /// </param>
        /// <param name="count">
        /// Total number of records available in the data source.
        /// </param>
        /// <param name="currentPage">
        /// Current page number being returned.
        /// </param>
        /// <param name="pageSize">
        /// Number of items displayed per page.
        /// </param>
        public PagedList(IEnumerable<T> items, int count, int currentPage, int pageSize)
        {
            // Adds all paginated items to the internal list
            AddRange(items);

            // Stores the total number of records
            TotalCount = count;

            // Stores the current page number
            CurrentPage = currentPage;

            // Stores the number of items displayed per page
            PageSize = pageSize;

            // Calculates the total number of pages
            // using ceiling division
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
        }

        /// <summary>
        /// Creates a paginated list from an IQueryable source.
        /// 
        /// Uses LINQ Skip and Take methods to retrieve
        /// only the records belonging to the requested page.
        /// 
        /// Because the source is IQueryable, the pagination
        /// is executed directly in the database, improving performance.
        /// </summary>
        /// <param name="source">
        /// IQueryable data source used for pagination.
        /// </param>
        /// <param name="currentPage">
        /// Current page number requested.
        /// </param>
        /// <param name="pageSize">
        /// Number of records displayed per page.
        /// </param>
        /// <returns>
        /// A PagedList containing the paginated records
        /// and pagination metadata.
        /// </returns>
        public static PagedList<T> ToPagedList(IQueryable<T> source, int currentPage, int pageSize)
        {
            // Retrieves the total number of records
            // from the data source
            var count = source.Count();

            // Skips records from previous pages
            // and retrieves only the items
            // belonging to the current page
            var items = source
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            // Creates and returns a new paginated list
            return new PagedList<T>(items, count, currentPage, pageSize);
        }
    }
}