using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApi.Persistence.Entities;

namespace WebApi.Persistence.Configuration;
public class SellerConfig : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> entity)
    {
        entity.ToTable("Sellers");

        entity.HasKey(e => e.SellerId).HasName("PK__Sellers__7FE3DB81152A48DA");

        entity.Property(e => e.SellerId).ValueGeneratedNever();
        entity.Property(e => e.Location).HasMaxLength(100);
        entity.Property(e => e.Name).HasMaxLength(100);
        entity.Property(e => e.Password).HasMaxLength(100);
        entity.Property(e => e.Username).HasMaxLength(50);
    }
}

