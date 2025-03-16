using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantHandler(ILogger<DeleteRestaurantHandler> _logger,
    IRestaurantAuthorizationService restaurantAuthorizationService,IRestaurantsRepository _restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deleting Restaurant with Id : {request.id}");

        var restaurant = await _restaurantsRepository.GeyRestaurantById(request.id);
        if (restaurant == null) { throw new NotFoundException($"Restaurant with that id: {request.id} does not exist  "); }

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            throw new ForbidenException();

        await _restaurantsRepository.DeleteRestaurant(restaurant);
        


    }
}
