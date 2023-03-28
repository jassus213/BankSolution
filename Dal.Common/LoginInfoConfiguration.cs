using Authentication.Dal.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Common;

public class LoginInfoConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Login).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.Provider).IsRequired();
        builder.HasIndex(x => new
        {
            x.Login, x.Provider
        });
    }
}