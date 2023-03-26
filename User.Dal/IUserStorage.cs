namespace User.Dal;

public interface IUserStorage
{
    Task<IEnumerable<User.Entity.User>> GetAsyncByName(IEnumerable<string> names, CancellationToken token);
    Task<IEnumerable<User.Entity.User>> GetAsyncByIds(IEnumerable<string> Ids, CancellationToken token);
}