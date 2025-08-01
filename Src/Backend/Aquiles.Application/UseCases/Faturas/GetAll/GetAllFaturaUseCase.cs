using Aquiles.Communication.Responses.Faturas;
using Aquiles.Domain.Repositories.Clientes;
using Aquiles.Domain.Repositories.Faturas;
using Aquiles.Domain.Repositories.Planos;
using AutoMapper;

namespace Aquiles.Application.UseCases.Faturas.GetAll;
public class GetAllFaturaUseCase : IGetAllFaturaUseCase
{
    private readonly IMapper _mapper;
    private readonly IFaturaReadOnlyRepository _readOnlyRepository;

    public GetAllFaturaUseCase(
        IMapper mapper,
        IFaturaReadOnlyRepository readOnlyRepository,
        IClienteReadOnlyRepository clienteReadOnlyRepository,
        IPlanoReadOnlyRepository readPlanoOnlyRepository)
    {
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    public async Task<IList<ResponseFaturaJson>> Execute(Guid? idPlano, Guid? clienteId)
    {
        var faturas = await _readOnlyRepository.GetAll(idPlano, clienteId);
        return _mapper.Map<IList<ResponseFaturaJson>>(faturas);
    }
}
