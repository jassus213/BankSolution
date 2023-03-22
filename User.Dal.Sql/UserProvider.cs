using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace User.Dal.Sql;

public class UserProvider : IUserProvider
{
    
    private readonly IDbContextFactory<UserContext> _contextFactory;

    public UserProvider(IDbContextFactory<UserContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public Task<IEnumerable<User>> GetAsync(IEnumerable<string> names, CancellationToken token)
    {
        using var context = _contextFactory.CreateDbContext();

        var usersRequest = context.Users.Where(x => names.Contains(x.Name))
            .Select(Map)
            .ToArray();
        var result = usersRequest.ToArray();
        return Task.FromResult((IEnumerable<User>)result);
    }

    private User Map(UserInfo.UserInfo data)
    {
        var result = new User
        {
            Id = data.Id,
            Name = data.Name,
            SecondName = data.SecondName
        };

        return result;
    }
}