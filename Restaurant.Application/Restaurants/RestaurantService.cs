using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants;

internal class RestaurantService(IRestaurantsRepository _restaurantsRepository
    , ILogger<RestaurantService> _Logger) : IRestaurantService
{

    public async Task<IEnumerable<RestaurantModel>> GetAllRestaurants()
    {
        _Logger.LogInformation("Getting all Restaurants (we are in Restaurant Service )");
        var restaurants = await _restaurantsRepository.GetAllAsync();
        return restaurants;
    }

    public async Task<RestaurantModel?> GetById(Guid Id)
    {
        _Logger.LogInformation($"Getting Restaurant with Id : {Id}");
        var restaurant = await _restaurantsRepository.GeyRestaurantById(Id);
        return restaurant;
    }
}
