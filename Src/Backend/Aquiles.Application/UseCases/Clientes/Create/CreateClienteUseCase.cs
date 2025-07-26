using Aquiles.Communication.Requests.Clientes;
using Aquiles.Communication.Responses.Clientes;
using Aquiles.Domain.Entities;
using Aquiles.Domain.Repositories;
using Aquiles.Domain.Repositories.Clientes;
using Aquiles.Exception.AquilesException;
using AutoMapper;

namespace Aquiles.Application.UseCases.Clientes.Create;
public class CreateClienteUseCase : ICreateClienteUseCase
{
    private readonly IClienteWriteOnlyRepository _clienteWriteOnlyRepository;
    private readonly IClienteReadOnlyRepository _clienteReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClienteUseCase(
        IClienteWriteOnlyRepository clienteWriteOnlyRepository,
        IClienteReadOnlyRepository clienteReadOnlyRepository,
        IMapper mapper, 
        IUnitOfWork unitOfWork)
    {
        _clienteWriteOnlyRepository = clienteWriteOnlyRepository;
        _clienteReadOnlyRepository = clienteReadOnlyRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseClientesJson> Execute(RequestCreateClientesJson request)
    {
        await Validate(request);
        var cliente = _mapper.Map<Cliente>(request);
        cliente.Id = Guid.NewGuid();
        await _clienteWriteOnlyRepository.AddAsync(cliente);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ResponseClientesJson>(cliente);
    }

    private async Task Validate(RequestCreateClientesJson request)
    {
        var result = new ClienteValidator().Validate(request);
        var codeExist = await _clienteReadOnlyRepository.ExistClienteWithCode(request.Codigo);

        if (codeExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("código", "Cóodigo do cliente já cadastrado na base de dados"));
        }

        if (!result.IsValid)
        {
            var mensagensDeErro = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ValidationErrorException(mensagensDeErro);
        }
    }
}
