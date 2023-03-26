using Authentication.Entity;

namespace Authentication.Dal;

public interface IUserInfoProvider
{
    Task<IEnumerable<UserLoginInfo>> GetAsyncById(IEnumerable<string> ids, CancellationToken token);
    Task<IEnumerable<UserLoginInfo>> GetAsyncByLogin(IEnumerable<string> logins, CancellationToken token);
}