using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.User.Commands.AssignUserRole;
using Restaurant.Application.User.Commands.DeleteUserRole;
using Restaurant.Application.User.Commands.UpdateUserDetails;
using Restaurant.Domain.Constants;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IMediator _mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            await _mediator.Send(command);
            return NoContent();


        }

        [HttpPost("UserRole")]
        [Authorize(Roles =UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("DeleteUserRole")]
        [Authorize(Roles =UserRoles.Admin)]
        public async Task<IActionResult> DeleteUserRole(DeleteUserRoleCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


    }
}
