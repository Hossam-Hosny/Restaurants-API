using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.DTOs;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> _logger,
    IRestaurantsRepository _restaurantRepository,IMapper _mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDTO>
{
    public async Task<DishDTO> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Retrieving dish: {request.DishId} From Restaurant with that id : {request.RestaurantId} ");

        var restaurant = await _restaurantRepository.GeyRestaurantById(request.RestaurantId)
            ?? throw new NotFoundException($"Restaurant with that id: {request.RestaurantId} not exist");

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId)
            ?? throw new NotFoundException($"Dish with that id:{request.DishId} not exist");

        var result = _mapper.Map<DishDTO>(dish);
        return result;


    }
}
