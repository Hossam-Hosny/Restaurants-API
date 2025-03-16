using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurants.Commands.CreateRestaurant;
using Restaurant.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurant.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurant.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurant.Application.Restaurants.Queries.GetAllRestaurants.GetById;
using Restaurant.Domain.Constants;
using Restaurant.Infrastructure.Authorization;


namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantsController(IMediator _mediator) : ControllerBase
    {

        [HttpGet("Get-All-Restauratns")]
        //[AllowAnonymous]
        [Authorize(Policy =PolicyNames.CreatedAtLeast2Restaurants)]
        
        public async Task<IActionResult> GetAll()
        {
            var result =await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(result);
        }

        [HttpGet("{Id:guid}")]
        [Authorize(Policy = PolicyNames.HasNationality)]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {

           

            var result = await _mediator.Send(new GetByIdQuery(Id));
           
            return Ok(result);
        }

        [HttpPost("Create-Restaurant")]
        [Authorize(Roles =UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand dto )
        {

            Guid id = await _mediator.Send(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);

        }

        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteRestaurant( [FromRoute] Guid Id)
        {

            await _mediator.Send(new DeleteRestaurantCommand(Id)); 

         return NoContent(); 

          

        }

        [HttpPatch("{Id:guid}")]
        public async Task<IActionResult> UpdateRestaurant( [FromRoute] Guid Id ,[FromBody] UpdateRestaurantCommand model)
        {
            model.Id = Id;
            await _mediator.Send(model);

             return NoContent(); 

            


        }


    }
}
