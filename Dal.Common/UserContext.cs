using Authentication.Dal.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dal.Common;

public class UserContext : DbContext
{
    public DbSet<User.Dal.Entity.User> Users { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new LoginInfoConfiguration());
    }
}