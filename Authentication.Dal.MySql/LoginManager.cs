using Authentication.Core.Utils;
using Authentication.Dal;
using User.Dal.Interfaces;
using User.Factories;

namespace Authentication.Core;

public class LoginManager : ILoginManager
{
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly IAuthenticationManager _authenticationManager;
    private readonly AuthenticationFactory _userLoginFactory;
    private readonly UserInfoFactory _userInfoFactory;
    private readonly IUserManager _userManager;
    private readonly ITokenManager _tokenManager;

    public LoginManager(IAuthenticationProvider authenticationProvider, IAuthenticationManager authenticationManager,
        ITokenManager tokenManager, IUserManager userManager, AuthenticationFactory userLoginFactory, UserInfoFactory userInfoFactory)
    {
        _authenticationProvider = authenticationProvider;
        _authenticationManager = authenticationManager;
        _tokenManager = tokenManager;
        _userManager = userManager;
        _userLoginFactory = userLoginFactory;
        _userInfoFactory = userInfoFactory;
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


        var userLogin = _userLoginFactory.Create(login, CryptUtils.ComputeHash(password), provider);

        var user = _userInfoFactory.Create(name, secondName);
        
        await _authenticationManager.AddUsersAsync(new[] { userLogin }, default);
        await _userManager.AddUsersAsync(new[] { user }, token);
        return await AsyncLogin(login, password, provider, token);
    }
}