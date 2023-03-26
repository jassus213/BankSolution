using Authentication.Entity;

namespace Authentication.Dal.Sql;

public class UserInfoProvider : IUserInfoProvider
{
    public async Task<IEnumerable<UserLoginInfo>> GetAsyncById(IEnumerable<string> ids, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserLoginInfo>> GetAsyncByLogin(IEnumerable<string> logins, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}