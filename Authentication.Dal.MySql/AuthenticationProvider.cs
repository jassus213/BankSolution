using Authentication.Dal.Mapper;
using Authentication.Entity;
using Dal.Common;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Dal.Sql;

public class AuthenticationProvider : IAuthenticationProvider
{
    private readonly IDbContextFactory<UserContext> _contextFactory;

    public AuthenticationProvider(IDbContextFactory<UserContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<UserLoginInfo>> GetAsyncById(IEnumerable<int> ids, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);

        var usersRequest = await context.UserLogins.Where(x => ids.Contains(x.Id)).ToArrayAsync(cancellationToken: token);

        var result = usersRequest.Select(AuthenticationMapper.Map).ToArray();
        return result;
    }

    public async Task<IEnumerable<UserLoginInfo>> GetAsyncByLogin(IEnumerable<string> logins, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);

        var usersRequest = await context.UserLogins.Where(x => logins.Contains(x.Login)).ToArrayAsync(cancellationToken: token);

        var result = usersRequest.Select(AuthenticationMapper.Map).ToArray();
        return result;
    }
    
}