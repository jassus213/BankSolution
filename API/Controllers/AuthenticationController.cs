using Authentication;
using Authentication.Core;
using Authentication.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController
{
    private readonly ILogger<UsersController> _logger;
    private readonly ILoginManager _loginManager;

    public AuthenticationController(ILogger<UsersController> logger, ILoginManager loginManager)
    {
        _logger = logger;
        _loginManager = loginManager;
    }

    [HttpPost("LoginUser")]
    public async Task<string> LoginUser([FromBody] AuthenticationEntity authentication, CancellationToken token)
    {
        var result =
            await _loginManager.AsyncLogin(authentication.Login, authentication.Password, Providers.Internal, token);

        return result;
    }

    [HttpPost("RegisterUser")]
    public async Task<string> RegisterUser([FromBody] RegisterEntity userRequest, CancellationToken token)
    {
        var result = await _loginManager.AsyncRegister(userRequest.Login, userRequest.Password, Providers.Internal,
            userRequest.Name, userRequest.SecondName, token);

        return result;
    }
}