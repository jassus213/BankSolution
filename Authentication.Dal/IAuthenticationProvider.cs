using Authentication.Entity;

namespace Authentication.Dal;

public interface IAuthenticationProvider
{
    Task<IEnumerable<UserLoginInfo>> GetAsyncById(IEnumerable<int> ids, CancellationToken token);
    Task<IEnumerable<UserLoginInfo>> GetAsyncByLogin(IEnumerable<string> logins, CancellationToken token);
}