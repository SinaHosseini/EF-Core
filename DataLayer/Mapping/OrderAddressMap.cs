using DomainLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Mapping
{
    public class OrderAddressMap : IEntityTypeConfiguration<OrderAddress>
    {
        public void Configure(EntityTypeBuilder<OrderAddress> builder)
        {
            builder.HasKey(c => c.OrderAddressId);
            builder.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(b => b.Order)
                .WithOne(b => b.OrderAddress)
                .HasForeignKey<OrderAddress>(b => b.OrderId);
        }
    }
}
