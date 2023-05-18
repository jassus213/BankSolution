using Authentication.Core.Entity;
using Authentication.Dal.Mapper;
using Dal.Common;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Dal.Sql;

public class AuthenticationProvider : IAuthenticationProvider
{
    private readonly IDbContextFactory<AuthenticationContext> _contextFactory;

    public AuthenticationProvider(IDbContextFactory<AuthenticationContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<AuthenticationInfo>> GetAsyncById(IEnumerable<int> ids, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);

        var usersRequest = await context.Authentication.Where(x => ids.Contains(x.Id)).ToArrayAsync(cancellationToken: token);

        var result = usersRequest.Select(AuthenticationMapper.Map).ToArray();
        return result;
    }

    public async Task<IEnumerable<AuthenticationInfo>> GetAsyncByLogin(IEnumerable<string> logins, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);

        var usersRequest = await context.Authentication.Where(x => logins.Contains(x.Login)).ToArrayAsync(token);

        var result = usersRequest.Select(AuthenticationMapper.Map).ToArray();
        return result;
    }
    
}