using Aquiles.Application.UseCases.Clientes.AtivarOuInativar;
using Aquiles.Application.UseCases.Clientes.Create;
using Aquiles.Application.UseCases.Clientes.GetAll;
using Aquiles.Application.UseCases.Clientes.Update;
using Aquiles.Application.UseCases.Usuarios.Create;
using Microsoft.Extensions.DependencyInjection;

namespace Aquiles.Application;
public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        AddUsuarioUseCase(services);
        AddClienteUseCase(services);
    }

    private static void AddUsuarioUseCase(IServiceCollection services)
    {
        services.AddScoped<ICreateUsuarioUseCase, CreateUsuarioUseCase>();
    }

    private static void AddClienteUseCase(IServiceCollection services) 
    {
        services
            .AddScoped<ICreateClienteUseCase, CreateClienteUseCase>()
            .AddScoped<IGetAllClientesUseCase, GetAllClientesUseCase>()
            .AddScoped<IUpdateClienteUseCase,  UpdateClienteUseCase>()
            .AddScoped<IAtivarOuInativarClienteUseCase, AtivarOuInativarClienteUseCase>();
    }
}
