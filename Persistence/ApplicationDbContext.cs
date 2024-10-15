using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public sealed class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<User> Users {get;set;}
        public DbSet<Wallet> Wallets {get;set;}
        public DbSet<Author> Authors {get;set;}
        public DbSet<ProfileMaterial> ProfileMaterial { get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        }
    }
}
