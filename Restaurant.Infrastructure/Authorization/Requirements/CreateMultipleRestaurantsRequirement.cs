using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Infrastructure.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirement(int minimumRestaurantsCreated) : IAuthorizationRequirement
{
    public int MinimumRestaurantsCreated { get; } = minimumRestaurantsCreated;

}
