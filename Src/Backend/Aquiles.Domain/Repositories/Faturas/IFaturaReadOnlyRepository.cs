using Aquiles.Domain.Entities;

namespace Aquiles.Domain.Repositories.Faturas;
public interface IFaturaReadOnlyRepository
{
    public Task<Fatura> GetById(Guid id);
    Task<IList<Fatura>> GetAll(Guid? idPlano, Guid? clienteId);
}
