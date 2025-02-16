using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.DbContexts;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Infrastructure.Repositories;

internal class RestaurantsRepository(AppDbContext _db) : IRestaurantsRepository
{
    public async Task<IEnumerable<RestaurantModel>> GetAllAsync()
    {
        var restaurants = await _db.Restaurants.Include("Dishes").ToListAsync();
        return restaurants;
    }

    public async Task<RestaurantModel?> GeyRestaurantById(Guid id)
    {
        RestaurantModel? Restaurant = await _db.Restaurants
                                .Include("Dishes")
                                .FirstOrDefaultAsync(r => r.Id == id);
        return Restaurant;
    }

    public async Task<Guid> CreateRestaurant(RestaurantModel restaurant)
    {
      await  _db.Restaurants.AddAsync(restaurant);
      await  _db.SaveChangesAsync();

        



        return restaurant.Id;


    }

  

    public async Task DeleteRestaurant(RestaurantModel model)
    {
        _db.Restaurants.Remove(model);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateRestaurant(RestaurantModel model)
    {
        _db.Restaurants.Update(model);
        await _db.SaveChangesAsync();
    }
}
