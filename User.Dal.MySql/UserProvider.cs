using Dal.Common;
using Microsoft.EntityFrameworkCore;
using User.Dal.Interfaces;
using User.Dal.Mapper;


namespace User.Dal.MySql;

public class UserProvider : IUserProvider
{
    
    private readonly IDbContextFactory<UserContext> _contextFactory;

    public UserProvider(IDbContextFactory<UserContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    

    public async Task<IEnumerable<User.Entity.UserInfo>> GetAsyncByName(IEnumerable<string> names, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);
        
        var usersRequest = await context.Users.Where(x => names.Contains(x.Name))
            .ToArrayAsync(cancellationToken: token);
        
        var result = usersRequest.Select(UserMapper.Map).ToArray();
        return result;
    }

    public async Task<IEnumerable<User.Entity.UserInfo>> GetAsyncByIds(IEnumerable<int> ids, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);
        
        var usersRequest = await context.Users.Where(x => ids.Contains(x.Id))
            .ToArrayAsync(cancellationToken: token);
        
        var result = usersRequest.Select(UserMapper.Map).ToArray();
        return result;
    }

   
    
}