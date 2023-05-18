using Authentication.Core.Entity;
using Authentication.Dal.Mapper;
using Dal.Common;
using Microsoft.EntityFrameworkCore;
using User.Dal.MySql;

namespace Authentication.Dal.Sql;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly IDbContextFactory<AuthenticationContext> _contextFactory;

    public AuthenticationManager(IDbContextFactory<AuthenticationContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<int>> AddUsersAsync(IEnumerable<AuthenticationInfo> authenticationInfos,
        CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);

        var idList = new List<int>();

        foreach (var authenticationInfo in authenticationInfos)
        {
            await context.Authentication.AddAsync(AuthenticationMapper.Map(authenticationInfo), token);
            idList.Add(authenticationInfo.Id);
        }

        await context.SaveChangesAsync(token);
        return idList.ToArray();
    }
}