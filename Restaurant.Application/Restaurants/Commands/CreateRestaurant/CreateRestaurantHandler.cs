using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.User;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantHandler(ILogger<CreateRestaurantHandler> _Logger 
    ,IUserContext userContext
    , IMapper _mapper , IRestaurantsRepository _restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, Guid>
{
    public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();


        _Logger.LogInformation("We are in Create Service - Creating a new Restaurant" +
            "{UserEmail} with {UserId} Create Restaurant",currentUser.Email , currentUser.Id);


        var restaurant = _mapper.Map<RestaurantModel>(request);
        restaurant.OwnerId = currentUser.Id;

        foreach (var dish in restaurant.Dishes)
        {
            dish.RestaurantId = restaurant.Id;
        }


        Guid id = await _restaurantsRepository.CreateRestaurant(restaurant);

        return id;
    }
}
