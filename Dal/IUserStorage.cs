namespace User.Dal;

public interface IUserStorage
{
    Task<User> TryRegisterAsync(string login, string password, string name, string secondName, CancellationToken token);
    Task<User> TryLoginAsync(string login, string password, CancellationToken token);
    Task<IEnumerable<User>> GetAsyncByName(IEnumerable<string> names, CancellationToken token);
    Task<IEnumerable<User>> GetAsyncByIds(IEnumerable<string> Ids, CancellationToken token);
}