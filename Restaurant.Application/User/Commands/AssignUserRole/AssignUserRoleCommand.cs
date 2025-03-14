using MediatR;

namespace Restaurant.Application.User.Commands.AssignUserRole;

public class AssignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; }
    public string RoleName { get; set; }
}
