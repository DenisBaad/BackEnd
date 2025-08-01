using Aquiles.Communication.Requests.Planos;

namespace Aquiles.Application.UseCases.Planos.Create;
public interface ICreatePlanoUseCase
{
    public Task Execute(RequestCreatePlanoJson request);
}
