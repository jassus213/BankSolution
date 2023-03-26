using Dal.Common;
using Microsoft.EntityFrameworkCore;
// ReSharper disable All


namespace User.Dal.MySql;

public class UserStorage : IUserStorage
{
    
    private readonly IDbContextFactory<UserContext> _contextFactory;

    public UserStorage(IDbContextFactory<UserContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    

    public async Task<IEnumerable<User.Entity.User>> GetAsyncByName(IEnumerable<string> names, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);
        
        var usersRequest = await context.Users.Where(x => names.Contains(x.Name))
            .ToArrayAsync(cancellationToken: token);
        
        var result = usersRequest.Select(Map).ToArray();
        return result;
    }

    public async Task<IEnumerable<User.Entity.User>> GetAsyncByIds(IEnumerable<string> ids, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);
        
        var usersRequest = await context.Users.Where(x => ids.Contains(x.Id))
            .ToArrayAsync(cancellationToken: token);
        
        var result = usersRequest.Select(Map).ToArray();
        return result;
    }

    private User.Entity.User Map(Entity.User data)
    {
        var result = new User.Entity.User
        {
            Id = data.Id,
            Name = data.Name,
            SecondName = data.SecondName,
            Login = data.Login,
            Password = data.Password
        };

        return result;
    }
    
}