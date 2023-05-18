using Authentication.Core.Entity;

namespace Authentication.Dal;

public class AuthenticationFactory
{
    public AuthenticationInfo Create(string login, string password, string provider)
    {
        return new AuthenticationInfo
        {
            Login = login,
            Password = password,
            Provider = provider
        };
    }
}