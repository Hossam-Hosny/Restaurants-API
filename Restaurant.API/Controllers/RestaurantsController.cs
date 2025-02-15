using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Restaurant.Application.Restaurants;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IRestaurantService _restaurantService) : ControllerBase
    {

        [HttpGet("Get-All-Restauratns")]
        public async Task<IActionResult> GetAll()
        {
            var result =await _restaurantService.GetAllRestaurants();
            return Ok(result);
        }

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var result = await _restaurantService.GetById(Id);
            if (result is null) { return NotFound($"No Restaurant with that id: {Id}"); }
            return Ok(result);
        }




    }
}
