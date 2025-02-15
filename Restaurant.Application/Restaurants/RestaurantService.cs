using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants;

internal class RestaurantService(IRestaurantsRepository _restaurantsRepository
    , ILogger<RestaurantService> _Logger , IMapper _mapper) : IRestaurantService
{
    public async Task<Guid> Create(CreateRestaurantDTO dto)
    {

        _Logger.LogInformation("We are in Create Service - Creating a new Restaurant");

        var restaurant = _mapper.Map<RestaurantModel>(dto);

        foreach(var dish in restaurant.Dishes)
        {
            dish.RestaurantId = restaurant.Id;
        }

        
      Guid id = await _restaurantsRepository.CreateRestaurant(restaurant);

        return id;
    }

    public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurants()
    {
        _Logger.LogInformation("Getting all Restaurants (we are in Restaurant Service )");
        var restaurants = await _restaurantsRepository.GetAllAsync();

        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);



        return restaurantsDto;
    }

    public async Task<RestaurantDTO?> GetById(Guid Id)
    {
        _Logger.LogInformation($"Getting Restaurant with Id : {Id}");
        var restaurant = await _restaurantsRepository.GeyRestaurantById(Id);

        var resturnatDto = _mapper.Map<RestaurantDTO?>(restaurant);

        return resturnatDto;
    }
}
