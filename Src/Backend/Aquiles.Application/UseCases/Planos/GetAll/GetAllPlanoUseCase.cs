using Aquiles.Communication.Responses.Planos;
using Aquiles.Domain.Repositories.Planos;
using AutoMapper;

namespace Aquiles.Application.UseCases.Planos.GetAll;
public class GetAllPlanoUseCase : IGetAllPlanoUseCase
{
    private readonly IMapper _mapper;
    private readonly IPlanoReadOnlyRepository _readOnlyRepository;

    public GetAllPlanoUseCase(
        IMapper mapper, 
        IPlanoReadOnlyRepository readOnlyRepository)
    {
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    public async Task<IList<ResponsePlanoJson>> Execute()
    {
        var plano = await _readOnlyRepository.GetAll();
        return _mapper.Map<IList<ResponsePlanoJson>>(plano);
    }
}
