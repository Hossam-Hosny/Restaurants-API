
using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.DbContexts;

namespace Restaurant.Infrastructure.Seeders;

internal class RestaurantSeeder(AppDbContext _db) : IRestaurantSeeder
{

    public async Task Seed()
    {
        if (await _db.Database.CanConnectAsync())
        {
            if (!_db.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                _db.Restaurants.AddRange(restaurants);
                await _db.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<RestaurantModel> GetRestaurants()
    {
        List<RestaurantModel> restaurants =
            [
                new()
                {
                    Name = "KFC",
                    Category= "Fast Food",
                    Description="This is a description",
                    ContactEmail="Contact@Contact.com",
                     HasDelivery = true,
                     Dishes = [
                         new()  {
                             Name="Nashville Hot Chicken",
                             Description="This is a description",
                             Price = 10.30m

                         },
                         new(){
                            Name = "Chicken Nuggets",
                            Description = "This is a description",
                            Price=5.5m
                         },

                         ],
                     Address = new(){
                         City = "London",
                         PostalCode="this is a Postal Code ",
                         Street = "This is a London Street "
                     }
                },
                new()
                {
                    Name = "Mackdonald",
                    Category= "Fast Food",
                    Description="This is a description",
                    ContactEmail="Contact@Contact.com",
                     HasDelivery = true,
                     Dishes = [
                         new()  {
                             Name="Mackdonalld Hot Chicken",
                             Description="This is a description",
                             Price = 10.30m

                         },
                         new(){
                            Name = "Mackdonald Nuggets",
                            Description = "This is a description",
                            Price=5.5m
                         },

                         ],
                     Address = new(){
                         City = "Huawi",
                         PostalCode="this is a Postal Code ",
                         Street = "This is a London Street "
                     }
                },


            ];
        return restaurants;



    }
}
