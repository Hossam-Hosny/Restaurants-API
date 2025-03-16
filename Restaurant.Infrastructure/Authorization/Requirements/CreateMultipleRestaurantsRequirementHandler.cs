using Microsoft.AspNetCore.Authorization;
using Restaurant.Application.User;
using Restaurant.Domain.Repositories;

namespace Restaurant.Infrastructure.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirementHandler(IUserContext userContext,
    IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<CreateMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRestaurantsRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        var restaurants = await restaurantsRepository.GetAllAsync();

        var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser!.Id);

        if (userRestaurantsCreated > requirement.MinimumRestaurantsCreated)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

    }
}
