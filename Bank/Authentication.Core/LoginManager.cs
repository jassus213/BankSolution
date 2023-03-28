using Authentication.Dal;
using Authentication.Dal.Entity;
using Authentication.Utils;
using User.Dal.Interfaces;
using User.Entity;

namespace Authentication.Core;

public class LoginManager : ILoginManager
{
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly IAuthenticationManager _authenticationManager;
    private readonly IUserManager _userManager;
    private readonly ITokenManager _tokenManager;

    public LoginManager(IAuthenticationProvider authenticationProvider, Dal.IAuthenticationManager authenticationManager,
        ITokenManager tokenManager, IUserManager userManager)
    {
        _authenticationProvider = authenticationProvider;
        _authenticationManager = authenticationManager;
        _tokenManager = tokenManager;
        _userManager = userManager;
    }

    public async Task<string> AsyncLogin(string login, string password, string provider, CancellationToken token)
    {
        var users = await _authenticationProvider.GetAsyncByLogin(new[] { login }, token);
        var currentUser = users.FirstOrDefault();
        if (currentUser == null)
            return null;

        if (currentUser.Password == CryptUtils.ComputeHash(password))
            return _tokenManager.CreateToken(currentUser, TimeSpan.FromDays(1));

        return null;
    }

    public async Task<string> AsyncRegister(string login, string password, string provider, string name, string secondName, CancellationToken token)
    {
        var users = await _authenticationProvider.GetAsyncByLogin(new[] { login }, token);
        if (users.Any())
            return null;

        var userLogin = new UserLogin()
        {
            Login = login,
            Password = CryptUtils.ComputeHash(password),
            Provider = provider
        };

        var user = new UserInfo()
        {
            Name = name,
            SecondName = secondName
        };


        var ids = _authenticationManager.AddUsersAsync(new[] { userLogin }, default);
        await _userManager.AddUsersAsync(new[] { user }, token);
        return await AsyncLogin(login, password, provider, token);
    }
}