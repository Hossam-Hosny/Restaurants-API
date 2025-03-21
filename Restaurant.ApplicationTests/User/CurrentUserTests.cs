using FluentAssertions;
using Restaurant.Domain.Constants;
using Xunit;

namespace Restaurant.Application.User.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // Arrange
        var CurrentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin,UserRoles.User],null,null);

        // Act 
        var IsInRole = CurrentUser.IsInRole(roleName);


        // Assert

        IsInRole.Should().BeTrue();

    }

    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var CurrentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act 
        var IsInRole = CurrentUser.IsInRole(UserRoles.Owner);


        // Assert

        IsInRole.Should().BeFalse();

    }


    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // Arrange
        var CurrentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act 
        var IsInRole = CurrentUser.IsInRole(UserRoles.Admin.ToLower());


        // Assert

        IsInRole.Should().BeFalse();

    }








}