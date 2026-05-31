using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.Repositories.Interfaces;

namespace Criando_Minha_Primeira_API.UnitOfWork
{
    /// <summary>
    /// Defines the contract for the Unit of Work pattern.
    /// 
    /// The Unit of Work coordinates multiple repositories
    /// using a single shared DbContext instance.
    /// 
    /// It centralizes repository access and ensures that
    /// all changes are committed to the database
    /// as a single transaction.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Provides access to the specialized Category repository.
        /// 
        /// In addition to generic CRUD operations,
        /// this repository also contains Category-specific features
        /// such as filtering and pagination.
        /// </summary>
        ICategoryRepository RepositoryCategory { get; }

        /// <summary>
        /// Provides access to the specialized Product repository.
        /// 
        /// In addition to generic CRUD operations,
        /// this repository also contains Product-specific features
        /// such as filtering and pagination.
        /// </summary>
        IProductRepository RepositoryProducts { get; }

        /// <summary>
        /// Persists all pending changes to the database.
        /// 
        /// Executes SaveChanges on the shared DbContext instance,
        /// ensuring that all operations are committed together.
        /// </summary>
        void Commit();
    }
}