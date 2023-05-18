using Authentication.Core.Entity;

namespace Authentication.Dal;

public interface IAuthenticationProvider
{
    Task<IEnumerable<AuthenticationInfo>> GetAsyncById(IEnumerable<int> ids, CancellationToken token);
    Task<IEnumerable<AuthenticationInfo>> GetAsyncByLogin(IEnumerable<string> logins, CancellationToken token);
}