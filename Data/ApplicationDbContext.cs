using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Depuntzak_V2.Models;

namespace Depuntzak_V2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Depuntzak_V2.Models.Product> Product { get; set; } = default!;

        public DbSet<Depuntzak_V2.Models.Menu> Menu { get; set; } = default!;

        public DbSet<Depuntzak_V2.Models.Transaction> Transaction { get; set; } = default!;

        public DbSet<Depuntzak_V2.Models.ProductTransaction> ProductTransaction { get; set; } = default!;
    }
}