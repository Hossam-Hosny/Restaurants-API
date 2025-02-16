using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantHandler(ILogger<CreateRestaurantHandler> _Logger , IMapper _mapper , IRestaurantsRepository _restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, Guid>
{
    public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {

        _Logger.LogInformation("We are in Create Service - Creating a new Restaurant");

        var restaurant = _mapper.Map<RestaurantModel>(request);

        foreach (var dish in restaurant.Dishes)
        {
            dish.RestaurantId = restaurant.Id;
        }


        Guid id = await _restaurantsRepository.CreateRestaurant(restaurant);

        return id;
    }
}
