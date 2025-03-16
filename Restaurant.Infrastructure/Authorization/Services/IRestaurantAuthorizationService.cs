using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.Authorization.Services
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(RestaurantModel restaurant, ResourceOperation operation);
    }
}