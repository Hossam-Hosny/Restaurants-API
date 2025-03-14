using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Application.Dishes.Commands.DeleteDishes;
using Restaurant.Application.Dishes.DTOs;
using Restaurant.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurant.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurant.Infrastructure.Authorization;

namespace Restaurant.API.Controllers
{
    [Route("api/Restaurants/{restaurantId}/[controller]")]
    [ApiController]
    [Authorize]
    public class DishesController(IMediator _mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] Guid restaurantId , CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            await _mediator.Send(command);
            return Created();
            
        }

        [HttpGet]
        [Authorize(Policy =PolicyNames.AtLeast20)]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetAllForRestaurant([FromRoute] Guid restaurantId)
        {
            var dishes = await _mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);


        }


        [HttpGet("{dishId:int}")]
        public async Task<ActionResult<DishDTO>> GetByIdForRestaurant([FromRoute] Guid restaurantId, int dishId)
        {

            var dish = await _mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId,dishId));

            return Ok(dish);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute]Guid restaurantId)
        {

            await _mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
            return NoContent();


        }


    }
}
