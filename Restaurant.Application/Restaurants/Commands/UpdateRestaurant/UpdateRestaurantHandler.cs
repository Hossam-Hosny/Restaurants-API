using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantHandler(ILogger<UpdateRestaurantHandler> _logger ,IMapper _mapper, IRestaurantsRepository _repository
    ,IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating Restaurant with Id :{request.Id}");

        RestaurantModel? restaurant = await _repository.GeyRestaurantById(request.Id);
        if (restaurant == null) { throw new NotFoundException($"Restaurant with that id: {request.Id} does not exist  "); }


        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidenException();

        _mapper.Map(request, restaurant);


       
        await _repository.UpdateRestaurant(restaurant);
        
    }
}
