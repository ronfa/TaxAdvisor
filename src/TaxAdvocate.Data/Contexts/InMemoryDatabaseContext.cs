using Microsoft.EntityFrameworkCore;
using TaxAdvocate.Data.Model;

namespace TaxAdvocate.Data.Contexts
{
    public class InMemoryDatabaseContext : DbContext 
    {
        public InMemoryDatabaseContext(DbContextOptions<InMemoryDatabaseContext> options)
            : base(options) { }

        public DbSet<Mutation> Mutations { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mutation>()
                .HasOne(p => p.Client)
                .WithMany(b => b.Mutations)
                .HasForeignKey(p => p.ClientId)
                .HasPrincipalKey(c => c.ClientId);
        }

    }
}
