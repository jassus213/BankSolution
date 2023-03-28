using Authentication.Entity;

namespace Authentication;

public interface ITokenManager
{
    public string CreateToken(UserLoginInfo user, TimeSpan lifetime);
}