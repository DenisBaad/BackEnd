using Aquiles.Domain.Entities;
using Aquiles.Domain.Repositories.Usuarios;
using Aquiles.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Aquiles.Infrastructure.Repositories;
public class UsuarioRepository : IUsuarioWriteOnlyRepository, IUsuarioReadOnlyRepository
{
    private readonly AquilesContext _context;

    public UsuarioRepository(AquilesContext context) => _context = context;

    public async Task AddAsync(Usuario usuario) => await _context.Usuarios.AddAsync(usuario);

    public async Task<bool> ExistUserByEmail(string email) =>  await _context.Usuarios.AsNoTracking().AnyAsync(x => x.Email.Equals(email));
}
