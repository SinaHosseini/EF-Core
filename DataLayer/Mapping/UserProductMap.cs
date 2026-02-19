using DomainLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Mapping
{
    public class UserProductMap : IEntityTypeConfiguration<UserProduct>
    {
        public void Configure(EntityTypeBuilder<UserProduct> builder)
        {
            builder.HasKey(b => b.UserProductId);
            builder.Property(b => b.Color)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(b => b.User)
                .WithMany(b => b.UserProducts)
                .HasForeignKey(b => b.UserId);

            builder.HasOne(b => b.Product)
                .WithMany(b => b.UserProducts)
                .HasForeignKey(b => b.ProductId);
        }
    }
}
