using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurant.Application.User;

public  class UserContext(IHttpContextAccessor httpContextAccessor):IUserContext
{

    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User
            ?? throw new InvalidOperationException("User Context is not Present");


        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return null;

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        
        var DateOfBirthAsString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
        var DateOfBirth = DateOfBirthAsString == null
            ? (DateOnly?)null
            : DateOnly.ParseExact(DateOfBirthAsString, "yyyy-MM-dd");


        return new CurrentUser(userId,email,roles , nationality,DateOfBirth);

    }


}
