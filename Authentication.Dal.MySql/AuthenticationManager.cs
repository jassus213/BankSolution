using Authentication.Dal.Entity;
using Dal.Common;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Dal.Sql;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly IDbContextFactory<UserContext> _contextFactory;
    
    public AuthenticationManager(IDbContextFactory<UserContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    public async Task<IEnumerable<int>> AddUsersAsync(IEnumerable<UserLogin> userLoginInfos, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);

        var idList = new List<int>();

        foreach (var userLoginInfo in userLoginInfos)
        {
            await context.UserLogins.AddAsync(userLoginInfo, token);
            idList.Add(userLoginInfo.Id);
        }

        await context.SaveChangesAsync(token);
        return idList.ToArray();
    }
}