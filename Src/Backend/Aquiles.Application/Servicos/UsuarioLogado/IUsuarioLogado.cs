namespace Aquiles.Application.Servicos.UsuarioLogado;
public interface IUsuarioLogado
{
    Task<Guid?> GetUsuario();
}
