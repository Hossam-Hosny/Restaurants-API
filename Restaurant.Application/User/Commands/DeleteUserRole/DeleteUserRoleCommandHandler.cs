using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.User.Commands.DeleteUserRole;

public class DeleteUserRoleCommandHandler(ILogger<DeleteUserRoleCommandHandler> _logger,
    UserManager<Domain.Entities.User> _userManager,
    RoleManager<IdentityRole> _roleManager) : IRequestHandler<DeleteUserRoleCommand>
{
    public async Task Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deleting User with email: {request.UserEmail} from Role: {request.RoleName}");

        var user = await _userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException($"User with email:{request.UserEmail} not exist");

        var role = await _roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException($"Role with Name:{request.RoleName} not exist");


        await _userManager.RemoveFromRoleAsync(user,role.Name!);



    }
}
