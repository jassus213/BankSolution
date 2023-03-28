namespace User.Dal.Interfaces;

public interface IUserProvider
{
    Task<IEnumerable<User.Entity.UserInfo>> GetAsyncByName(IEnumerable<string> names, CancellationToken token);
    Task<IEnumerable<User.Entity.UserInfo>> GetAsyncByIds(IEnumerable<int> ids, CancellationToken token);
}