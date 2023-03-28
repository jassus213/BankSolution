using Authentication.Dal.Entity;

namespace Authentication.Dal;

public interface IAuthenticationManager
{
    Task<IEnumerable<int>> AddUsersAsync(IEnumerable<UserLogin> userLoginInfos, CancellationToken token);
}