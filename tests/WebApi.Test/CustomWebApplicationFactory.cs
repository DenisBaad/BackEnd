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
    private Usuario _usuario;
    private string _senha;

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

                (_usuario, _senha) = Seed(database);
            });
    }

    private static (Usuario usuario, string senha) Seed(AquilesContext context)
    {
        (var usuario, string senha) = UsuarioBuilder.Build();
        context.Usuarios.Add(usuario);
        context.SaveChanges();
        return (usuario, senha);
    }

    public Usuario GetUsuario()
    {
        return _usuario;
    }

    public string GetSenha()
    {
        return _senha;
    }
}
