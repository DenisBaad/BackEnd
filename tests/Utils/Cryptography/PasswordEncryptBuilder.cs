using Aquiles.Application.Servicos;

namespace CommonTestUtilities.Cryptography;
public class PasswordEncryptBuilder
{
    public static PasswordEncrypt Build() => new PasswordEncrypt("abc1234");
}
