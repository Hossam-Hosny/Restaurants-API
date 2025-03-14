
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.DbContexts
{
    internal class AppDbContext(DbContextOptions<AppDbContext> options): IdentityDbContext<User>(options)
    {
        
      

        internal DbSet<RestaurantModel> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<RestaurantModel>()
                                        .OwnsOne(r => r.Address);

            modelBuilder.Entity<RestaurantModel>()
                                        .HasMany(r => r.Dishes)
                                        .WithOne()
                                        .HasForeignKey(d => d.RestaurantId);



            modelBuilder.Entity<User>()
                .HasMany(o => o.OwnedRestaurants)
                .WithOne(r => r.Owner)
                .HasForeignKey(r => r.OwnerId);

        }


    }
}
