using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Dal;

public class UserConfiguration : IEntityTypeConfiguration<UserInfo.UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo.UserInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        builder.Property(x => x.SecondName);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Login);
        builder.Property(x => x.Password);
    }
}