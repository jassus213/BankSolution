using Microsoft.EntityFrameworkCore;


namespace User.Dal.MySql;

public class UserStorage : IUserStorage
{
    
    private readonly IDbContextFactory<UserContext> _contextFactory;

    public UserStorage(IDbContextFactory<UserContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<User> TryRegisterAsync(string login, string password, string name, string secondName, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);

        var user = new UserInfo.UserInfo
        {
            Id = Guid.NewGuid().ToString(),
            Login = login,
            Password = MyPasswordHasher.Hash(password),
            Name = name,
            SecondName = secondName
        };

        await context.Users.AddAsync(user, token);
        await context.SaveChangesAsync(token);
        
        return Map(user);
    }
    

    public async Task<User> TryLoginAsync(string login, string password, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);

        var user = await context.Users.Where(x => x.Login == login).FirstAsync(cancellationToken: token);
        if (!MyPasswordHasher.Validate(user.Password, password))
            return null;
        
        return Map(user);
    }

    public async Task<IEnumerable<User>> GetAsyncByName(IEnumerable<string> names, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);
        
        var usersRequest = await context.Users.Where(x => names.Contains(x.Name))
            .ToArrayAsync(cancellationToken: token);
        
        var result = usersRequest.Select(Map).ToArray();
        return result;
    }

    public async Task<IEnumerable<User>> GetAsyncByIds(IEnumerable<string> ids, CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);
        
        var usersRequest = await context.Users.Where(x => ids.Contains(x.Id))
            .ToArrayAsync(cancellationToken: token);
        
        var result = usersRequest.Select(Map).ToArray();
        return result;
    }

    private User Map(UserInfo.UserInfo data)
    {
        var result = new User
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