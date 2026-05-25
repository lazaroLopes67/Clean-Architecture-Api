using Criando_Minha_Primeira_API.Context;
using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.Repositories.Interfaces;
using Criando_Minha_Primeira_API.Repositories;

namespace Criando_Minha_Primeira_API.UnitOfWork
{
    /// <summary>
    /// Implements the Unit of Work pattern, coordinating the work of multiple repositories
    /// by sharing a single database context instance.
    /// Ensures that all database operations are committed together as a single transaction.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The application's database context used to track and persist changes.
        /// Shared across all repositories to ensure consistency.
        /// </summary>
        private readonly AppDbContext _context;
        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The database context injected via dependency injection.</param>
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Lazy-initialized repository for Category entities.
        /// Ensures the repository is created only when first accessed.
        /// </summary>
        private IRepository<Category>? _repositoryCategory;

        /// <summary>
        /// Lazy-initialized repository for Product entities.
        /// Ensures the repository is created only when first accessed.
        /// </summary>
        private IRepository<Product>? _repositoryProducts;

        /// <summary>
        /// Provides access to the Category repository.
        /// Creates a new instance if it has not been initialized yet.
        /// </summary>
        public IRepository<Category> RepositoryCategory
        {
            get
            {
                return _repositoryCategory ??= new Repository<Category>(_context);
            }
        }
        /// <summary>
        /// Provides access to the Product repository.
        /// Creates a new instance if it has not been initialized yet.
        /// </summary>
        public IRepository<Product> RepositoryProducts
        {
            get
            {
                return _repositoryProducts ??= new Repository<Product>(_context);
            }
        }
        /// <summary>
        /// Persists all changes made through the repositories to the database.
        /// Executes SaveChanges on the shared DbContext, ensuring atomicity.
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
