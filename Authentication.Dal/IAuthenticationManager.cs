using Authentication.Entity;

namespace Authentication.Dal;

public interface IAuthenticationManager
{
    Task<IEnumerable<int>> AddUsersAsync(IEnumerable<UserLoginInfo> userLoginInfos, CancellationToken token);
}