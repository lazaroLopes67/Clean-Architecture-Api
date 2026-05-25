using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.Repositories.Interfaces;

namespace Criando_Minha_Primeira_API.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Category> RepositoryCategory { get; }
        IRepository<Product> RepositoryProducts{ get; }
        void Commit();
    }
}
