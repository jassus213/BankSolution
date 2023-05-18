using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Core.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Core;

public class TokenManager : ITokenManager
{
    private readonly IOptions<AuthenticationOptions> _configuration;

    public TokenManager(IOptions<AuthenticationOptions> configuration)
    {
        _configuration = configuration;
    }
    
    
    public string CreateToken(AuthenticationInfo user, TimeSpan lifetime)
    {
        if (_configuration == null)
            throw new ApplicationException("Configuration missing");

        var configuration = _configuration.Value;
        
        if (string.IsNullOrEmpty(configuration.Secret))
            throw new ArgumentNullException(nameof(configuration.Secret));
        if (string.IsNullOrEmpty(configuration.Secret))
            throw new ArgumentNullException(nameof(configuration.Secret));

        var tokenHandler = new JwtSecurityTokenHandler();

        var securitySigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Secret));
        var signingCredentials = new SigningCredentials(securitySigningKey, SecurityAlgorithms.HmacSha256);

        var identity = CreateIdentity(user);
        if (identity == null)
            return null;

        var now = DateTime.UtcNow;

        var jwt = tokenHandler.CreateJwtSecurityToken(
            configuration.Issuer,
            configuration.Audience,
            identity,
            now.AddHours(-1),
            DateTime.UtcNow.Add(lifetime),
            now,
            signingCredentials
        );

        return tokenHandler.WriteToken(jwt);
    }
    
    private static ClaimsIdentity CreateIdentity(AuthenticationInfo user)
    {
        if (user == null)
            return null;

        var culture = CultureInfo.InvariantCulture;
        var claims = new[]
        {
            //new Claim(JwtRegisteredClaimNames.Email, user.Email ?? String.Empty),
            //new Claim(JwtRegisteredClaimNames.GivenName, user.DisplayName ?? String.Empty),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
            new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString(culture)),
            new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(culture), ClaimValueTypes.Integer64),
        };

        return new ClaimsIdentity(claims, "Token");
    }
    
    private static long ToUnixEpochDate(DateTime date)
    {
        return (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}