using DomainLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(b => b.ProductId);
            builder.Property(b => b.ProductName)
                .IsRequired()
                .HasMaxLength(80);
            builder.Property(b => b.ImageName)
                .IsRequired()
                .HasMaxLength(110);
            builder.Property(b => b.ProductDescription)
                .IsRequired();
        }
    }
}
