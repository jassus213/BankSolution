using System.Security.Cryptography;
using System.Text;

namespace Authentication.Utils;

public class CryptUtils
{
    private const string CONST_SALT = "b1901c1bbd1777ad8ec8";

    public static string ComputeHash(string password)
    {
        using (var encrypter = SHA256.Create())
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password + CONST_SALT);

            var hash = encrypter.ComputeHash(bytes);

            return string.Join("", hash.Select(b => b.ToString("x2")));
        }
    }
}