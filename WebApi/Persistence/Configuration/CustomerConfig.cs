using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Persistence.Entities;
using static Dapper.SqlMapper;

namespace WebApi.Persistence.Configuration;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.ToTable("Customers");
        entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D85F00DE36");

        entity.Property(e => e.CustomerId).ValueGeneratedNever();
        entity.Property(e => e.Password).HasMaxLength(100);
        entity.Property(e => e.Username).HasMaxLength(50);
    }
}
