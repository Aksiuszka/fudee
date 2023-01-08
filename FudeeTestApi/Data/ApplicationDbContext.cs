using FudeeTestApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FudeeTestApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Restaurant>? Restaurants { get; set; }
        public DbSet<Address>? Addresses { get; set; } 
        public DbSet<Dish>? Dishes { get; set; }
        public DbSet<Opinion>? Opinions { get; set;}
        public DbSet<AppUser>? AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Category>()
                .HasMany(c => c.Restaurants)
                .WithOne(r => r.Category);

            modelbuilder.Entity<Restaurant>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Restaurants);

            modelbuilder.Entity<Address>()
                .HasOne(a => a.Restaurant)
                .WithOne(r => r.Address);

            modelbuilder.Entity<Restaurant>()
                .HasOne(r => r.Address)
                .WithOne(a => a.Restaurant);

            modelbuilder.Entity<Dish>()
                .HasOne(d => d.Restaurant)
                .WithMany(r => r.Dishes)
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Restaurant>()
                .HasMany(r => r.Dishes)
                .WithOne(d => d.Restaurant);

            modelbuilder.Entity<Opinion>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Opinions)
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Restaurant>()
                .HasMany(r => r.Opinions)
                .WithOne(o => o.Restaurant);

            modelbuilder.Entity<AppUser>()
                .HasMany(u => u.Restaurants)
                .WithOne(r => r.User);

            modelbuilder.Entity<Restaurant>()
                .HasOne(r => r.User)
                .WithMany(u => u.Restaurants);

            modelbuilder.Entity<AppUser>()
                .HasMany(u => u.Opinions)
                .WithOne(o => o.User);

            modelbuilder.Entity<Opinion>()
                .HasOne(o => o.User)
                .WithMany(u => u.Opinions);




        }
    }
}