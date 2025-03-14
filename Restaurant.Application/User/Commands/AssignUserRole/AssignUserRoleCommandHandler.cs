using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.User.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> _logger,
    UserManager<Domain.Entities.User> _userManager,
    RoleManager<IdentityRole> _roleManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Assigning User role: {@Request}",request);
        var user = await _userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException($"User with this email:{request.UserEmail} not exist ") ;

        var role = await _roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException($"Role with this Name:{request.RoleName} not exist ");


        await _userManager.AddToRoleAsync(user, role.Name!);



    }
}
