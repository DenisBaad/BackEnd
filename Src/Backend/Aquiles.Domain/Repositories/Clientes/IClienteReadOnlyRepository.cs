using Aquiles.Domain.Entities;

namespace Aquiles.Domain.Repositories.Clientes;
public interface IClienteReadOnlyRepository
{
    public Task<IList<Cliente>> GetAll();
    public Task<bool> ExistClienteWithCode(int code);
}
