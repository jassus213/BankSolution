using Dal.Common;
using Microsoft.EntityFrameworkCore;

namespace User.Dal.MySql;

public class UserContext : DbContext
{
    public DbSet<User.Dal.Entity.User> Users { get; set; }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
    }
}