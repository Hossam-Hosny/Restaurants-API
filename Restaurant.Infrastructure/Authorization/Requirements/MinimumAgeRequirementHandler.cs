using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurant.Application.User;

namespace Restaurant.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> _logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        _logger.LogInformation("User: {Email}, date of birth {DoB} - Handling MinimumAgeRequirement"
            ,currentUser.Email,
            currentUser.DateOfBirth);

        if (currentUser.DateOfBirth is  null)
        {
            _logger.LogWarning("User date of Birth is null");
            context.Fail();
            return Task.CompletedTask;
        }

        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            _logger.LogInformation("Authorization Succeeded");
            context.Succeed(requirement);
        }

        context.Fail();
        return Task.CompletedTask;

    }
}
