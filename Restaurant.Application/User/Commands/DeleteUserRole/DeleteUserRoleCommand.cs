using MediatR;

namespace Restaurant.Application.User.Commands.DeleteUserRole;

public class DeleteUserRoleCommand:IRequest
{
    public string UserEmail { get; set; }
    public string RoleName { get; set; }
}
