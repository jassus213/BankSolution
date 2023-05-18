using Authentication.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Dal.Sql;

public class AuthenticationConfiguration : IEntityTypeConfiguration<AuthenticationInfo>
{
    public void Configure(EntityTypeBuilder<AuthenticationInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Login);
        builder.Property(x => x.Password);
    }
}