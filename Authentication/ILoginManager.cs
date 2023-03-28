namespace Authentication;

public interface ILoginManager
{
    Task<string> AsyncLogin(string login, string password, string provider, CancellationToken token);
    Task<string> AsyncRegister(string login, string password, string provider, string name, string secondName, CancellationToken token);
}