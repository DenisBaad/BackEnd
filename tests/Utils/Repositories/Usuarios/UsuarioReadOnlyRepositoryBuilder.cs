using Aquiles.Domain.Repositories.Usuarios;
using Moq;

namespace CommonTestUtilities.Repositories.Usuarios;
public class UsuarioReadOnlyRepositoryBuilder
{
    private readonly Mock<IUsuarioReadOnlyRepository> _repository;

    public UsuarioReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUsuarioReadOnlyRepository>();
    }
    
    public IUsuarioReadOnlyRepository Build()
    {
        return _repository.Object;
    }

    public void ExistUserByEmail(string email) 
    {
        _repository.Setup(repository => repository.ExistUserByEmail(email)).ReturnsAsync(true);
    }
}
