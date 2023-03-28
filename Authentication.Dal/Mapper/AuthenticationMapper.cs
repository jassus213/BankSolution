using Authentication.Dal.Entity;
using Authentication.Entity;

namespace Authentication.Dal.Mapper;

public static class AuthenticationMapper
{
    public static UserLoginInfo Map(UserLogin userLogin)
    {
        return new UserLoginInfo()
        {
            Id = userLogin.Id,
            Login = userLogin.Login,
            Password = userLogin.Password,
            Provider = userLogin.Provider
        };
    }
}