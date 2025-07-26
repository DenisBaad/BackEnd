using Aquiles.Communication.Requests.Clientes;
using Aquiles.Communication.Requests.Usuarios;
using Aquiles.Communication.Responses.Clientes;
using Aquiles.Communication.Responses.Usuarios;
using Aquiles.Domain.Entities;
using AutoMapper;

namespace Aquiles.Application.Servicos;
public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        UsuarioMap();
        ClienteMap();
    }

    private void UsuarioMap()
    {
        CreateMap<RequestCreateUsuariosJson, Usuario>().ReverseMap();
        CreateMap<Usuario, ResponseUsuariosJson>().ReverseMap();
    }

    private void ClienteMap()
    {
        CreateMap<RequestCreateClientesJson, Cliente>().ReverseMap();
        CreateMap<Cliente, ResponseClientesJson>().ReverseMap();
    }
}
