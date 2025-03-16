using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> _logger,
    IRestaurantsRepository _restaurantsRepository , 
    IDishRepository _dishesRepository , IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Removing all dishes from  Restaurant with Id:{request.RestaurantId}");
        var restaurant = await _restaurantsRepository.GeyRestaurantById(request.RestaurantId)
            ?? throw new NotFoundException($"Restaurant with Id:{request.RestaurantId} not exist");

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidenException();


        await _dishesRepository.Delete(restaurant.Dishes);

    }
}
