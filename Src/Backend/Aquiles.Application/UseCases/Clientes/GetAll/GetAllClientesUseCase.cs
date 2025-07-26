using Aquiles.Communication.Responses.Clientes;
using Aquiles.Domain.Repositories.Clientes;
using AutoMapper;

namespace Aquiles.Application.UseCases.Clientes.GetAll;
public class GetAllClientesUseCase : IGetAllClientesUseCase
{
    private readonly IClienteReadOnlyRepository _clienteReadOnlyRepository;
    private readonly IMapper _mapper;

    public GetAllClientesUseCase(
        IClienteReadOnlyRepository clienteReadOnlyRepository,
        IMapper mapper)
    {
        _clienteReadOnlyRepository = clienteReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<IList<ResponseClientesJson>> Execute()
    {
        var cliente = await _clienteReadOnlyRepository.GetAll();
        return _mapper.Map<IList<ResponseClientesJson>>(cliente);
    }
}

