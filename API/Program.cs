using System.Text;
using Authentication;
using Authentication.Core;
using Authentication.Dal;
using Authentication.Dal.Sql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using User.Dal.Interfaces;
using User.Dal.MySql;
using User.Factories;
using AuthenticationManager = Authentication.Dal.Sql.AuthenticationManager;
using IAuthenticationManager = Authentication.Dal.IAuthenticationManager;


var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile("passwordsalt.json")
    .AddJsonFile("authenticationoptions.json")
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .Build();



#region Db

var connectionString = configuration.GetConnectionString("Users");

builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContextFactory<UserContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Scoped);


builder.Services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(connectionString!));

builder.Services.AddDbContextFactory<AuthenticationContext>(options => options.UseSqlServer(connectionString!),
    ServiceLifetime.Scoped);

#endregion

#region Common

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region Authentication

builder.Services.AddScoped<ILoginManager, LoginManager>();
builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<IAuthenticationProvider, AuthenticationProvider>();
builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>    
    {    
        var jwtSettings = configuration.GetSection("JwtConfig").Get<AuthenticationOptions>();
        options.TokenValidationParameters = new TokenValidationParameters    
        {    
            ValidateIssuer = true,    
            ValidateAudience = true,    
            ValidateLifetime = true,    
            ValidateIssuerSigningKey = true,    
            ValidIssuer = jwtSettings.Issuer,    
            ValidAudience = jwtSettings.Audience,    
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))    
        };
    });

builder.Services.Configure<AuthenticationOptions>(configuration.GetSection("JwtConfig"));
builder.Services.AddSingleton<AuthenticationFactory>();

#endregion

#region User

builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddSingleton<UserInfoFactory>();

#endregion

#region App

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    var useSwaggerUi = app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion



