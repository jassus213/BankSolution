using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Common;

public class UserConfiguration : IEntityTypeConfiguration<User.Dal.Entity.User>
{
    public void Configure(EntityTypeBuilder<User.Dal.Entity.User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        builder.Property(x => x.SecondName);
        builder.HasIndex(x => x.Name);
    }
}
