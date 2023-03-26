using Authentication.Dal.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Common;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Login);
        builder.Property(x => x.Password);
        builder.Property(x => x.Provider);
    }
}