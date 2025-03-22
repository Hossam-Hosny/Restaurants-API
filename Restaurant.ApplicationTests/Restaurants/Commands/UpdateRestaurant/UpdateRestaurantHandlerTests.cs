using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Xunit;

namespace Restaurant.Application.Restaurants.Commands.UpdateRestaurant.Tests;

public class UpdateRestaurantHandlerTests
{
    private readonly Mock<ILogger<UpdateRestaurantHandler>> _logger;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IRestaurantAuthorizationService> _authorizationService;
    private readonly UpdateRestaurantHandler _handler;

    public UpdateRestaurantHandlerTests(Mock<ILogger<UpdateRestaurantHandler>> logger
        , Mock<IRestaurantsRepository> restaurantsRepository
        , Mock<IMapper> mapper
        , Mock<IRestaurantAuthorizationService> authorizationService
        )
    {
        _logger = logger;
        _restaurantsRepository = restaurantsRepository;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _handler = new UpdateRestaurantHandler
            (
               _logger.Object
               ,_mapper.Object
               ,restaurantsRepository.Object
               ,_authorizationService.Object
            );
    }

    [Fact()]
    public async Task Handle_ForValidCommand_ShouldUpdateRestaurantSuccessfully()
    {
        // arrange
        var id = Guid.NewGuid();
        var command = new UpdateRestaurantCommand()
        {
            Id = id,
            Name = "new test",
            Description = "new description",
            HasDelivery = true
        };

        var restaurant = new RestaurantModel()
        {
            Id = id,
            Name="test",
            Description="test"
        };

        _restaurantsRepository.Setup(r => r.GeyRestaurantById(id))
            .ReturnsAsync(restaurant);

        _authorizationService.Setup(m => m.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
                                .Returns(true);

        // act 
        await _handler.Handle(command, CancellationToken.None);

        // assert
        _mapper.Verify(m => m.Map(command, restaurant), Times.Once);
        


    }


    [Fact()]
    public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
    {
        // arrange

        var id = Guid.NewGuid();
        var request = new UpdateRestaurantCommand
        {
            Id = id
        };


        _restaurantsRepository.Setup(r => r.GeyRestaurantById(id))
            .ReturnsAsync((RestaurantModel?)null);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert 

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with id: {id} does not exist");











    }


}