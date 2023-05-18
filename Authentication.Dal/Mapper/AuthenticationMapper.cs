using Authentication.Core.Entity;

namespace Authentication.Dal.Mapper;

public static class AuthenticationMapper
{
    public static AuthenticationInfo Map(Core.Entity.Authentication userLogin)
    {
        return new AuthenticationInfo()
        {
            Id = userLogin.Id,
            Login = userLogin.Login,
            Password = userLogin.Password,
            Provider = userLogin.Provider
        };
    }

    public static Core.Entity.Authentication Map(AuthenticationInfo authenticationInfo)
    {
        return new Core.Entity.Authentication
        {
            Id = authenticationInfo.Id,
            Login = authenticationInfo.Login,
            Password = authenticationInfo.Password,
            Provider = authenticationInfo.Provider
        };
    }
}