using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurant.Domain.Entities;
using System.Security.Claims;

namespace Restaurant.Infrastructure.Authorization;

public class RestaurantsUserClaimPrincipleFactory(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options)
    :UserClaimsPrincipalFactory<User,IdentityRole>(userManager,roleManager,options)
{

    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        
        var id = await GenerateClaimsAsync(user);
        if (user.Nationality is not null)
        {
            id.AddClaim(new Claim(AppClaimTypes.HasNationality,user.Nationality));
        }
        if (user.DateOfBirth is not null)
        {
            id.AddClaim(new Claim(AppClaimTypes.DateOfBirth,user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }


        return new ClaimsPrincipal(id);


    }






}
