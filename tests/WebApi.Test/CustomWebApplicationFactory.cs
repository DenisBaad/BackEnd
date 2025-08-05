using Aquiles.Domain.Entities;
using Aquiles.Infrastructure.Context;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;
public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AquilesContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<AquilesContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var scopeService = scope.ServiceProvider;
                var database = scopeService.GetRequiredService<AquilesContext>();

                database.Database.EnsureDeleted();

                (_usuario, _senha, _plano) = Seed(database);
            });
    }

    private Usuario _usuario;
    private string _senha;
    private Plano _plano;

    private static (Usuario usuario, string senha, Plano plano) Seed(AquilesContext context)
    {
        (var usuario, string senha) = UsuarioBuilder.Build();
        var plano = PlanoBuilder.Build(usuario.Id);
        context.Usuarios.Add(usuario);
        context.Planos.Add(plano);
        context.SaveChanges();
        return (usuario, senha, plano);
    }

    public Usuario GetUsuario()
    {
        return _usuario;
    }

    public string GetSenha()
    {
        return _senha;
    }

    public Plano GetPlano()
    {
        return _plano;
    }
}
