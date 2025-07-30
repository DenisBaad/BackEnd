using Aquiles.Application.Servicos;
using Aquiles.Application.UseCases.Clientes.AtivarOuInativar;
using Aquiles.Application.UseCases.Clientes.Create;
using Aquiles.Application.UseCases.Clientes.GetAll;
using Aquiles.Application.UseCases.Clientes.Update;
using Aquiles.Application.UseCases.Login.DoLogin;
using Aquiles.Application.UseCases.Usuarios.Create;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Aquiles.Application.Servicos.UsuarioLogado;
using Aquiles.Application.UseCases.Clientes.Delete;

namespace Aquiles.Application;
public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddRepositories(services);
        AdicionarChaveAdicionalToken(services, configurationManager);
        AdicionarTokenJWT(services, configurationManager);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        AddUsuarioUseCase(services);
        AddLoginUseCase(services);
        AdicionarUsuarioLogadoUseCase(services);
        AddClienteUseCase(services);
    }

    private static void AddUsuarioUseCase(IServiceCollection services)
    {
        services.AddScoped<ICreateUsuarioUseCase, CreateUsuarioUseCase>();
    }

    private static void AddLoginUseCase(IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();
    }

    private static void AdicionarUsuarioLogadoUseCase(IServiceCollection services)
    {
        services.AddScoped<IUsuarioLogado, UsuarioLogado>();
    }

    private static void AddClienteUseCase(IServiceCollection services) 
    {
        services
            .AddScoped<ICreateClienteUseCase, CreateClienteUseCase>()
            .AddScoped<IGetAllClientesUseCase, GetAllClientesUseCase>()
            .AddScoped<IUpdateClienteUseCase,  UpdateClienteUseCase>()
            .AddScoped<IDeleteClienteUseCase, DeleteClienteUseCase>()
            .AddScoped<IAtivarOuInativarClienteUseCase, AtivarOuInativarClienteUseCase>();
    }

    private static void AdicionarChaveAdicionalToken(IServiceCollection services, IConfiguration configurationManager)
    {
        services.AddScoped(option => new PasswordEncrypt(configurationManager.GetSection("Configuracoes:Senha:ChaveAdicionalSenha").Value));
    }

    private static void AdicionarTokenJWT(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(option =>
            new TokenController(
                configuration.GetSection("Configuracoes:Jwt:TokenKey").Value,
                int.Parse(configuration.GetSection("Configuracoes:Jwt:LifeTimeMinutes").Value)));

        services.AddScoped(option => new AquilesAuthorize(new TokenController(
                configuration.GetSection("Configuracoes:Jwt:TokenKey").Value,
                int.Parse(configuration.GetSection("Configuracoes:Jwt:LifeTimeMinutes").Value))));
    }
}
