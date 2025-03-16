using Microsoft.Extensions.Logging;
using Restaurant.Application.User;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> _logger,
    UserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(RestaurantModel restaurant, ResourceOperation operation)
    {

        var user = userContext.GetCurrentUser();

        _logger.LogInformation("Authorizing User {UserEmail}, to {Operation} for Restaurant {RestaurantName}",
            user.Email, operation, restaurant.Name);


        if (operation is ResourceOperation.Create || operation is ResourceOperation.Read)
        {
            _logger.LogInformation("Create/Read operation - Successful Authorization");
            return true;
        }

        if (operation is ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            _logger.LogInformation("Admin User, delete operation - successful authorization");
            return true;
        }

        if ((operation is ResourceOperation.Delete || operation is ResourceOperation.Update) && user.Id == restaurant.OwnerId)
        {
            _logger.LogInformation("Restaurant owner - successful authorization ");
            return true;
        }

        return false;

    }



}
