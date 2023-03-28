using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Dal.Interfaces;

namespace WebApplication1.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserProvider _userProvider;
    
    public UsersController(ILogger<UsersController> logger, IUserProvider userProvider)
    {
        _logger = logger;
        _userProvider = userProvider;
    }

    [HttpGet("{id:int}")]
    public async Task<IEnumerable<User.Entity.UserInfo>> GetInfoByIds([FromRoute] int id)
    {
        var result = await _userProvider.GetAsyncByIds(new []{id}, default);
        
        return result;
    }

}