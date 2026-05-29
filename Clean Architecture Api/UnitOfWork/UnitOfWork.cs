using Criando_Minha_Primeira_API.Context;
using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.Repositories.Interfaces;
using Criando_Minha_Primeira_API.Repositories;

namespace Criando_Minha_Primeira_API.UnitOfWork
{
    /// <summary>
    /// Implements the Unit of Work pattern.
    /// 
    /// This class coordinates the work of multiple repositories
    /// by sharing a single DbContext instance.
    /// 
    /// It ensures that all database operations are committed
    /// together as a single transaction.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Application database context used to track
        /// and persist entity changes.
        /// 
        /// The same DbContext instance is shared
        /// across all repositories managed by this UnitOfWork.
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">
        /// Database context injected through dependency injection.
        /// </param>
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lazy-initialized repository for Category entities.
        /// 
        /// The repository instance is only created
        /// when accessed for the first time.
        /// </summary>
        private IRepository<Category>? _repositoryCategory;

        /// <summary>
        /// Lazy-initialized repository for Product entities.
        /// 
        /// Uses the specialized ProductRepository implementation,
        /// which contains Product-specific operations
        /// such as filtering and pagination.
        /// 
        /// The repository instance is only created
        /// when accessed for the first time.
        /// </summary>
        private IProductRepository? _repositoryProducts;

        /// <summary>
        /// Provides access to the generic Category repository.
        /// 
        /// If the repository has not been instantiated yet,
        /// a new Repository<Category> instance is created.
        /// </summary>
        public IRepository<Category> RepositoryCategory
        {
            get
            {
                return _repositoryCategory ??=
                    new Repository<Category>(_context);
            }
        }

        /// <summary>
        /// Provides access to the specialized Product repository.
        /// 
        /// If the repository has not been instantiated yet,
        /// a new ProductRepository instance is created.
        /// 
        /// This repository supports Product-specific operations,
        /// including custom filtering and pagination methods.
        /// </summary>
        public IProductRepository RepositoryProducts
        {
            get
            {
                return _repositoryProducts ??=
                    new ProductRepository(_context);
            }
        }

        /// <summary>
        /// Persists all pending changes to the database.
        /// 
        /// Executes SaveChanges on the shared DbContext instance,
        /// ensuring that all operations are committed together.
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}