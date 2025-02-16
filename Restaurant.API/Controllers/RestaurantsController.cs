using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurants.Commands.CreateRestaurant;
using Restaurant.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurant.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurant.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurant.Application.Restaurants.Queries.GetAllRestaurants.GetById;
using System.Collections.Specialized;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IMediator _mediator) : ControllerBase
    {

        [HttpGet("Get-All-Restauratns")]
        public async Task<IActionResult> GetAll()
        {
            var result =await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(result);
        }

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var result = await _mediator.Send(new GetByIdQuery(Id));
            if (result is null) { return NotFound($"No Restaurant with that id: {Id}"); }
            return Ok(result);
        }

        [HttpPost("Create-Restaurant")]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand dto )
        {

            Guid id = await _mediator.Send(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);

        }

        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteRestaurant( [FromRoute] Guid Id)
        {

            var isDeleted = await _mediator.Send(new DeleteRestaurantCommand(Id)); 

            if (isDeleted) { return NoContent(); }

            return NotFound();

        }

        [HttpPatch("{Id:guid}")]
        public async Task<IActionResult> UpdateRestaurant( [FromRoute] Guid Id ,[FromBody] UpdateRestaurantCommand model)
        {
            model.Id = Id;
            var isUpdated = await _mediator.Send(model);

            if (isUpdated) { return NoContent(); }

            return NotFound($"Restaurant with that Id {Id} is not exist ");


        }


    }
}
