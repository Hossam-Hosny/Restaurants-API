using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using System.Security.Cryptography.Xml;

namespace Restaurant.Application.Dishes.Commands.CreateDish;

public class CreateDishHandler(ILogger<CreateDishHandler> _logger 
    , IRestaurantsRepository _restaurantRepository,
    IDishRepository _dishesRepository , IMapper _mapper) : IRequestHandler<CreateDishCommand>
{
    public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new dish: {@DishRequest}",request);

        var restaurant = await _restaurantRepository.GeyRestaurantById(request.RestaurantId)
            ?? throw new NotFoundException($"Restaurant With that Id:{request.RestaurantId} not exist ");

        var dish =  _mapper.Map<Dish>(request);
        await _dishesRepository.Create(dish);


    }
}
