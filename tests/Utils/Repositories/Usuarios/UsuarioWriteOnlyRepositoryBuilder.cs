using Aquiles.Domain.Repositories.Usuarios;
using Moq;

namespace CommonTestUtilities.Repositories.Usuarios;
public class UsuarioWriteOnlyRepositoryBuilder
{
    public static IUsuarioWriteOnlyRepository Build()
    {
        var mock = new Mock<IUsuarioWriteOnlyRepository>();

        return mock.Object;
    }
}
