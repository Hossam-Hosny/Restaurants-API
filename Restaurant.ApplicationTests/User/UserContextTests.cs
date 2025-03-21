using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurant.Domain.Constants;
using System.Security.Claims;
using Xunit;

namespace Restaurant.Application.User.Tests;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {

        // Arrange
        var httpContextAccessorMoq = new Mock<IHttpContextAccessor>();

        var UserContext = new UserContext(httpContextAccessorMoq.Object);

        var dateOfBirth = new DateOnly(2001, 1, 1);
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier,"1"),
            new (ClaimTypes.Email,"test@test.com"),
            new (ClaimTypes.Role,UserRoles.Admin),
            new (ClaimTypes.Role,UserRoles.User),
            new ("Nationality","Egyption"),
            new ("DateOfBirth",dateOfBirth.ToString("yyyy-MM-dd"))
        };
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims , "Test"));

        httpContextAccessorMoq.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user

        });


        // Act 

        var currentUser = UserContext.GetCurrentUser();



        // Assert

        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@test.com");
        currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);


    }

    [Fact]
    public void GetCurrentUser_WithUserContetNotPresent_ThrowsInvalidOperationException()
    {
        // Arrange 
        var httpContextAccessoreMoq = new Mock<IHttpContextAccessor>();
        httpContextAccessoreMoq.Setup(x => x.HttpContext).Returns((HttpContext)null);

        var userContext = new UserContext(httpContextAccessoreMoq.Object);

        // Act 
        Action action = () => userContext.GetCurrentUser();

        // Assert 
        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("User Context is not Present");




    }

}