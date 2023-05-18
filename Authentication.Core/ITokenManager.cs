using Authentication.Core.Entity;

namespace Authentication.Core;

public interface ITokenManager
{
    public string CreateToken(AuthenticationInfo user, TimeSpan lifetime);
}