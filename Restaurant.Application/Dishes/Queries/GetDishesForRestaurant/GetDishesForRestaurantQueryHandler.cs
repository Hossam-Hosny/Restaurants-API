using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.DTOs;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> _logger,
    IRestaurantsRepository _restaurantsRepository, IMapper _mapper) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDTO>>
{
    public async Task<IEnumerable<DishDTO>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Retrieving dishes for restaurnat with id : {request.RestaurantId}");

        var restaurant = await _restaurantsRepository.GeyRestaurantById(request.RestaurantId)
            ?? throw new NotFoundException($"Restaurant with this Id : {request.RestaurantId} not exist");

        var result = _mapper.Map<IEnumerable<DishDTO>>(restaurant.Dishes);
        return result;


    }
}
