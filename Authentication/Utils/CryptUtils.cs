using System.Security.Cryptography;
using System.Text;

namespace Authentication.Utils;

internal class CryptUtils
{
    private const string CONST_SALT = "b1901c1bbd1777ad8ec8";

    internal static string GenerateSalt()
    {
        using (var generator = new RNGCryptoServiceProvider())
        {
            var random = new byte[10];

            generator.GetBytes(random);

            return string.Join("", random.Select(b => b.ToString("x2")));
        }
    }

    internal static string ComputeHash(string password, string salt)
    {
        using (var encrypter = SHA256.Create())
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password + salt + CONST_SALT);

            var hash = encrypter.ComputeHash(bytes);

            return string.Join("", hash.Select(b => b.ToString("x2")));
        }
    }
}