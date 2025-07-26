namespace Aquiles.Domain.Repositories.Usuarios;
public interface IUsuarioReadOnlyRepository
{
    public Task<bool> ExistUserByEmail(string email);
}
