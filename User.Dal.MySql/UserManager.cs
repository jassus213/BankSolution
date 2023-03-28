using Dal.Common;
using Microsoft.EntityFrameworkCore;
using User.Dal.Interfaces;
using User.Dal.Mapper;
using User.Entity;

namespace User.Dal.MySql;

public class UserManager : IUserManager
{
    private readonly IDbContextFactory<UserContext> _contextFactory;

    public UserManager(IDbContextFactory<UserContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<int>> AddUsersAsync(IEnumerable<UserInfo> users, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);
        var ids = new List<int>();

        foreach (var user in users)
        {
            var entity = await context.Users.AddAsync(UserMapper.MapBack(user), token);
            ids.Add(entity.Entity.Id);
            
        }

        await context.SaveChangesAsync(token);
        return ids.ToArray();
    }
}