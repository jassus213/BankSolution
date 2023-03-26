using Microsoft.AspNetCore.Mvc;
using User.Dal;
using WebApplication1.Entity;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class UsersController
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserStorage _userStorage;
    
    public UsersController(ILogger<UsersController> logger, IUserStorage userStorage)
    {
        _logger = logger;
        _userStorage = userStorage;
    }
    
    [HttpGet("GetInfoByName")]
    public async Task<IEnumerable<User.Entity.User>> GetInfoByName([FromQuery]IEnumerable<string> names)
    {
        var result = await _userStorage.GetAsyncByName(names, default);
        
        return result;
    }
    
    [HttpGet("GetInfoByIds")]
    public async Task<IEnumerable<User.Entity.User>> GetInfoByIds([FromQuery] IEnumerable<string> ids)
    {
        var result = await _userStorage.GetAsyncByIds(ids, default);
        
        return result;
    }
    
    /*[HttpGet("LoginUser")]
    public async Task<User.Dal.Entity.User> TryLogin([FromQuery]LoginInfoUser infoUser)
    {
        var result = await _userStorage.TryLoginAsync(infoUser.Login, infoUser.Password, default);

        return result;
    }
        
    [HttpPost("RegisterUser")]
    public async Task<User.Dal.Entity.User> RegisterUser([FromBody]CreateUserRequest userRequest)
    {
        var result = await _userStorage.TryRegisterAsync(userRequest.Login, userRequest.Password, userRequest.Name, userRequest.SecondName,default);
        
        return result;
    }*/

}