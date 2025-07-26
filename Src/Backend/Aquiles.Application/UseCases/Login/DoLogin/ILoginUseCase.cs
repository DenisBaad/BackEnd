using Aquiles.Communication.Requests.Login;

namespace Aquiles.Application.UseCases.Login.DoLogin;
public interface ILoginUseCase
{
    public Task Execute(RequestLoginJson request);
}
