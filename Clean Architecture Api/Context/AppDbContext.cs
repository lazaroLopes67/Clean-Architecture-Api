using Criando_Minha_Primeira_API.Model;
using Microsoft.EntityFrameworkCore;
namespace Criando_Minha_Primeira_API.Context;

/// <summary>
/// Represents the application's database context and manages communication between the application and the database
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the AppDbContext class
    /// </summary>
    /// <param name="options">The options used to configure the database context</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    /// <summary>
    /// Represents the Categories table in the database
    /// </summary>
    public DbSet<Category> Categories { get; set; }
    /// <summary>
    /// Represents the Products table in the database
    /// </summary>
    public DbSet<Product> Products { get; set; }
}
