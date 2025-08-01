using Aquiles.Communication.Requests.Planos;
using Aquiles.Domain.Entities;
using Aquiles.Domain.Repositories.Planos;
using Aquiles.Domain.Repositories;
using Aquiles.Exception.AquilesException;
using AutoMapper;

namespace Aquiles.Application.UseCases.Planos.Create;
public class CreatePlanoUseCase : ICreatePlanoUseCase
{
    private readonly IMapper _mapper;
    private readonly IPlanoWriteOnlyRepository _planoWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreatePlanoUseCase(
        IMapper mapper, 
        IPlanoWriteOnlyRepository planoWriteRepository,
        IUnitOfWork unitOfWork)
    {
        _planoWriteRepository = planoWriteRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Execute(RequestCreatePlanoJson request)
    {
        Validate(request);
        var plano = _mapper.Map<Plano>(request);
        plano.Id = Guid.NewGuid();
        await _planoWriteRepository.Create(plano);
        await _unitOfWork.CommitAsync();
    }
    
    private void Validate(RequestCreatePlanoJson request)
    {
        var validator = new PlanoValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationErrorException(errorMessages);
        }
    }
}
