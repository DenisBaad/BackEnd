using Aquiles.Communication.Requests.Faturas;
using Aquiles.Domain.Entities;
using Aquiles.Domain.Repositories.Faturas;
using Aquiles.Domain.Repositories;
using Aquiles.Exception.AquilesException;
using AutoMapper;

namespace Aquiles.Application.UseCases.Faturas.Create;
public class CreateFaturaUseCase : ICreateFaturaUseCase
{
    private readonly IMapper _mapper;
    private readonly IFaturaWriteOnlyRepository _faturaWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateFaturaUseCase(
        IMapper mapper, 
        IFaturaWriteOnlyRepository faturaWriteRepository,
        IUnitOfWork unitOfWork)
    {
        _faturaWriteRepository = faturaWriteRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(RequestCreateFaturaJson request)
    {
        Validate(request);
        var fatura = _mapper.Map<Fatura>(request);
        fatura.Id = Guid.NewGuid();
        await _faturaWriteRepository.Create(fatura);
        await _unitOfWork.CommitAsync();
    }
    private void Validate(RequestCreateFaturaJson request)
    {
        var validator = new FaturaValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationErrorException(errorMessages);
        }
    }
}
