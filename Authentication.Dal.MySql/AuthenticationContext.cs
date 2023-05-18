using Authentication.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Dal.Sql;

public class AuthenticationContext : DbContext
{
    public DbSet<Core.Entity.Authentication> Authentication { get; set; }

    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AuthenticationConfiguration());
    }
}