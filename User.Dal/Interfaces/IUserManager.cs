using User.Entity;

namespace User.Dal.Interfaces;

public interface IUserManager
{
    public Task<IEnumerable<int>> AddUsersAsync(IEnumerable<UserInfo> users, CancellationToken token);
}