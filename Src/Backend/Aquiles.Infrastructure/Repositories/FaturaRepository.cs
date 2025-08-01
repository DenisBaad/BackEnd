using Aquiles.Domain.Entities;
using Aquiles.Domain.Repositories.Faturas;
using Aquiles.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Aquiles.Infrastructure.Repositories;
public class FaturaRepository : IFaturaWriteOnlyRepository, IFaturaReadOnlyRepository, IFaturaUpdateOnlyRepository
{
    private readonly AquilesContext _context;
    public FaturaRepository(AquilesContext context) => _context = context;

    public async Task Create(Fatura fatura) => await _context.Faturas.AddAsync(fatura);

    public async Task<Fatura> GetById(Guid id) => await _context.Faturas.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<IList<Fatura>> GetAll(Guid usuarioId, Guid? idPlano, Guid? clienteId)
    {
        var query = _context.Faturas.AsNoTracking().Where(x => x.UsuarioId == usuarioId).AsQueryable();

        if (idPlano.HasValue)
            query = query.Where(f => f.PlanoId == idPlano.Value);

        if (clienteId.HasValue)
            query = query.Where(f => f.ClienteId == clienteId.Value);

        return await query.ToListAsync();
    }
    
    async Task<Fatura> IFaturaUpdateOnlyRepository.GetById(Guid id) => await _context.Faturas.FirstOrDefaultAsync(x => x.Id == id);

    public void Update(Fatura fatura) => _context.Faturas.Update(fatura);

    public void Delete(Fatura fatura) => _context.Faturas.Remove(fatura);

}
