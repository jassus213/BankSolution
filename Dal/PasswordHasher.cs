using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace User.Dal.MySql;

public static class MyPasswordHasher
{
    private const int KeySize = 32;
    private const int Iterations = 10000;
    private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
    private const char SaltDelimiter = ';';
    
    public static string Hash(string password)
    {
        var json = File.ReadAllText("C:\\Users\\JaSSuS\\Projects\\WebApplication1\\Dal\\salt.json");
        dynamic? salt = JObject.Parse(json)["Salt"]?.ToString();
        byte[] byteSalt = Encoding.ASCII.GetBytes(salt);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, byteSalt, Iterations, _hashAlgorithmName, KeySize);
        return string.Join(SaltDelimiter, Convert.ToBase64String(byteSalt), Convert.ToBase64String(hash));
    }
    
    
    public static bool Validate(string passwordHash, string password)
    {
        var pwdElements = passwordHash.Split(SaltDelimiter);
        var salt = Convert.FromBase64String(pwdElements[0]);
        var hash = Convert.FromBase64String(pwdElements[1]);
        var hashInput = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);
        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}