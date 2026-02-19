using DomainLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "UM");

            builder.HasKey(b => b.UserId);
            builder.HasIndex(b => new { b.Email })
                .IsUnique();
            builder.Ignore(b => b.FullName);
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Family)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(b => b.CreateDate)
                .HasDefaultValue(DateTime.Now);
        }
    }
}
