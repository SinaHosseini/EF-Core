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

            builder.HasIndex(b => new { b.Email }).IsUnique();
            builder.HasKey(b => b.UserId);
            builder.Ignore(b => b.FullName);
            builder.Property(b => new { b.Name, b.Family })
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
