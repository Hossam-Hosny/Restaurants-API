using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.User.Commands;

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


    }
}
