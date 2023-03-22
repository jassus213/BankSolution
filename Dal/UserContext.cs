using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace User.Dal;

public class UserContext : DbContext
{
    public DbSet<UserInfo.UserInfo> Users { get; set; }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configBuilder = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfiguration configuration = configBuilder.Build();
        var userConnectionString = configuration.GetConnectionString("Users");
        Console.WriteLine(userConnectionString);
        
        optionsBuilder.UseSqlServer(userConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserInfo.UserInfo>().HasKey(x => x.Name);
        builder.Entity<UserInfo.UserInfo>().Property(x => x.SecondName);
        builder.Entity<UserInfo.UserInfo>().Property(x => x.Login);
        builder.Entity<UserInfo.UserInfo>().Property(x => x.Password);
    }
}