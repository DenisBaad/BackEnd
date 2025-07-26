using Aquiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquiles.Infrastructure.Context;
public class AquilesContext : DbContext
{
    public AquilesContext(DbContextOptions<AquilesContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AquilesContext).Assembly);
    }
}
