using System.Security.Cryptography;
using System.Text;

namespace Aquiles.Application.Servicos;
public class PasswordEncrypt
{
    public string HashPassword(string password)
    {
        using (var hmac = new HMACSHA256())
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedBytes = hmac.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var computedHash = HashPassword(password);
        return hashedPassword == computedHash;
    }
}
