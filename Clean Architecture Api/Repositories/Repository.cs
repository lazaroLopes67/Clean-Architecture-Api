using Criando_Minha_Primeira_API.Context;
using Criando_Minha_Primeira_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Criando_Minha_Primeira_API.Repositories
{
    /// <summary>
    /// Generic repository implementation that provides basic CRUD operations
    /// for any entity type using Entity Framework Core.
    /// This class centralizes data access logic and helps reduce code duplication
    /// across the application.
    /// </summary>
    /// <typeparam name="T">The entity type managed by this repository.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The application's database context used to interact with the database.
        /// It is injected via dependency injection.
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the generic repository.
        /// </summary>
        /// <param name="context">The database context provided by dependency injection.</param>
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Returns a queryable collection of all entities from the database.
        /// The query is not executed immediately, allowing the caller to
        /// further compose filters, sorting, and pagination before execution.
        /// </summary>
        /// <returns>
        /// An IQueryable of all entities of type T with tracking disabled
        /// for better performance in read-only scenarios.
        /// </returns>
        public IQueryable<T> GetAll()
        {
            // Retrieves the DbSet for the given entity type and disables tracking
            // to improve performance when entities are used in read-only operations.
            var items = _context.Set<T>().AsNoTracking();
            // Returns the query without executing it immediately (deferred execution)
            return items;
        }
        /// <summary>
        /// Retrieves a single entity that matches the specified condition.
        /// Uses AsNoTracking to improve performance for read-only queries.
        /// </summary>
        /// <param name="predicate">A lambda expression used to filter the entity.</param>
        /// <returns>The first matching entity, or null if none is found.</returns>
        public T? Get(Expression<Func<T, bool>> predicate)
        {
            var item = _context.Set<T>().AsNoTracking().FirstOrDefault(predicate);
            return item;
        }

        /// <summary>
        /// Adds a new entity to the database context.
        /// The entity is tracked and will be inserted into the database
        /// when SaveChanges is called on the context.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>The same entity instance that was added.</returns>
        public T Add(T entity)
        {
            _context.Add(entity);
            return entity;
        }

        /// <summary>
        /// Updates an existing entity in the database context.
        /// The entity state is marked as Modified and will be updated
        /// in the database when SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        /// <returns>The updated entity instance.</returns>
        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        /// <summary>
        /// Removes an entity from the database context.
        /// The entity will be deleted from the database when SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to be removed.</param>
        /// <returns>The removed entity instance.</returns>
        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }
    }
}