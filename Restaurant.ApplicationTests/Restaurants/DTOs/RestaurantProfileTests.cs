using AutoMapper;
using FluentAssertions;
using Restaurant.Application.Restaurants.Commands.CreateRestaurant;
using Restaurant.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurant.Domain.Entities;
using Xunit;

namespace Restaurant.Application.Restaurants.DTOs.Tests;

public class RestaurantProfileTests
{
    private IMapper _mapper;
    public RestaurantProfileTests()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<RestaurantProfile>();
        });

         _mapper = configuration.CreateMapper();
    }

    [Fact()]
    public void CreateMap_ForRestaurantToRestaurantDTO_MapsCorrectly()
    {
        // Arrange 
      

        var restuarant = new RestaurantModel
        {
            Id = Guid.NewGuid(),
            Name = "test",
            Description = "This is Description",

            Category = "test Category",
            Address=new Address
            {
                City = "test",
                PostalCode= "test",
                Street="test"
            },

            ContactEmail="testContactEmail",
            ContactNumber="test",
            HasDelivery = true
            
        };


        // Act 

        var restaurantDto = _mapper.Map<RestaurantDTO>(restuarant);

        // Assert 
        restaurantDto.Should().NotBeNull();
        restaurantDto.Name.Should().Be(restuarant.Name);
        restaurantDto.Description.Should().Be(restuarant.Description);

        restaurantDto.HasDelivery.Should().BeTrue();
        restaurantDto.City.Should().Be(restuarant.Address.City);
        restaurantDto.Street.Should().Be(restuarant.Address.Street);
        restaurantDto.PostalCode.Should().Be(restuarant.Address.PostalCode);




    }

    [Fact]
    public void CreateMap_ForRestaurantCreateCommandToRestaurant_MapsCorrectly()
    {
        // Arrange 
     

        var command = new CreateRestaurantCommand 
        {
            Name = "test",
            Description = "This is Description",
            Category = "test Category",
           

            ContactEmail = "testContactEmail",
            ContactNumber = "test",
            HasDelivery = true

        };


        // Act 

        var restaurant = _mapper.Map<RestaurantModel>(command);

        // Assert 
        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);

        restaurant.HasDelivery.Should().BeTrue();
        restaurant.Category.Should().Be(command.Category);
        restaurant.ContactNumber.Should().Be(command.ContactNumber);
        restaurant.ContactEmail.Should().Be(command.ContactEmail);
    



    }

    [Fact]
    public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // Arrange 

        var command = new UpdateRestaurantCommand
        {

            Name = "test",
            Description = "This is Description",
            HasDelivery = true


        };



        // Act 

        var restaurant = _mapper.Map<RestaurantModel>(command);

        // Assert 
        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);

        restaurant.HasDelivery.Should().BeTrue();
     




    }




}