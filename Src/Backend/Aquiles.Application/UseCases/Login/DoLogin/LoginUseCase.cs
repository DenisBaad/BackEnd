using Aquiles.Application.Servicos;
using Aquiles.Communication.Requests.Login;
using Aquiles.Domain.Repositories.Usuarios;
using Aquiles.Exception.AquilesException;

namespace Aquiles.Application.UseCases.Login.DoLogin;
public class LoginUseCase : ILoginUseCase
{
    private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
    private readonly PasswordEncrypt _passwordEncript;

    public LoginUseCase(
        IUsuarioReadOnlyRepository usuarioReadOnlyRepository, 
        PasswordEncrypt passwordEncript)
    {
        _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
        _passwordEncript = passwordEncript;
    }

    public async Task Execute(RequestLoginJson request)
    {
        try
        {
            var senhaCriptografada = _passwordEncript.Encript(request.Senha);
            var usuario = await _usuarioReadOnlyRepository.DoLogin(request.Email, senhaCriptografada);

            if (usuario is null)
            {
                throw new System.Exception("Login inválido.");
            }
        }
        catch (System.Exception ex)
        {
            if (ex is not InvalidLoginException)
            {
                throw new InvalidLoginException("E-mail ou senha inválidos.");
            }

            throw;
        }
    }
}
