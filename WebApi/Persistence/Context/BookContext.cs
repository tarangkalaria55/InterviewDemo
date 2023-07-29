using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApi.Persistence.Entities;

namespace WebApi.Persistence.Context
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        public IDbConnection Connection => Database.GetDbConnection();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

    }
}
