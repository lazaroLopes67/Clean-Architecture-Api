using System.Linq.Expressions;

namespace Criando_Minha_Primeira_API.Repositories.Interfaces
{
    /// <summary>
    /// Generic repository interface that defines basic CRUD operations
    /// for any entity type in the application.
    /// </summary>
    /// <typeparam name="T">The type of the entity managed by the repository.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Retrieves all entities from the data source.
        /// </summary>
        /// <returns>An enumerable collection containing all entities.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Retrieves a single entity that matches the specified condition.
        /// </summary>
        /// <param name="predicate">A function used to filter the entity.</param>
        /// <returns>The first matching entity.</returns>
        T? Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a new entity to the data source.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>The created entity.</returns>
        T Add(T entity);
        /// <summary>
        /// Updates an existing entity in the data source.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        T Update(T entity);

        /// <summary>
        /// Removes an entity from the data source.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        T Delete(T entity);
    }
}
