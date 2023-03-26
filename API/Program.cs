using Dal.Common;
using Microsoft.EntityFrameworkCore;
using User.Dal;
using User.Dal.MySql;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile("passwordsalt.json")
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .Build();

var connectionString = configuration.GetConnectionString("Users");

Console.WriteLine(connectionString);
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContextFactory<UserContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
builder.Services.AddScoped<IUserStorage, UserStorage>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    var useSwaggerUi = app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


