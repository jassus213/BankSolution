using Authentication.Entity;

namespace Authentication.Core.Factories;

public class UserLoginInfoFactory
{
    public UserLoginInfo Create(string login, string password, string provider)
    {
        return new UserLoginInfo()
        {
            Login = login,
            Password = password,
            Provider = provider
        };
    }
}