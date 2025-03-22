using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Application.User;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Xunit;

namespace Restaurant.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        // arrange 

        var loggerMock = new Mock<ILogger<CreateRestaurantHandler>>();
        var mapperMock = new Mock<IMapper>();

        var command = new CreateRestaurantCommand();
        var restaurant = new RestaurantModel();

        var id = Guid.NewGuid();

        mapperMock.Setup(m => m.Map<RestaurantModel>(command)).Returns(restaurant);

        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock
            .Setup(repo => repo.CreateRestaurant(It.IsAny<RestaurantModel>()))
            .ReturnsAsync(id);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "test@test.com", [],null,null);
        userContextMock.Setup(u => u.GetCurrentUser())
            .Returns(currentUser);





        var commandHandler = new CreateRestaurantHandler(loggerMock.Object
            ,userContextMock.Object
            ,mapperMock.Object
            ,restaurantRepositoryMock.Object
            );



        // act 

        var result = await commandHandler.Handle(command, CancellationToken.None);

        // assert 

        result.Should().Be(id);
        restaurant.OwnerId.Should().Be("owner-id");

        restaurantRepositoryMock.Verify(r => r.CreateRestaurant(restaurant), Times.Once);

    }
}