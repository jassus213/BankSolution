using Authentication.Core.Entity;

namespace Authentication.Dal;

public interface IAuthenticationManager
{
    Task<IEnumerable<int>> AddUsersAsync(IEnumerable<AuthenticationInfo> authenticationInfos, CancellationToken token);
}