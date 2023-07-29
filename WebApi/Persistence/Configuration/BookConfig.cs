using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
using WebApi.Persistence.Entities;
using static Dapper.SqlMapper;

namespace WebApi.Persistence.Configuration;

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> entity)
    {
        entity.ToTable("Books");

        entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207F07FFB6C");

        entity.Property(e => e.BookId).ValueGeneratedNever();
        entity.Property(e => e.Author).HasMaxLength(50);
        entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        entity.Property(e => e.Title).HasMaxLength(100);

        entity.HasOne(d => d.Seller).WithMany(p => p.Books)
            .HasForeignKey(d => d.SellerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Books_Sellers");
    }
}
