using Microsoft.AspNetCore.Mvc;
using RestaurantRaterAPI.Models;

namespace RestaurantRaterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private RestaurantDBContext _context;
        public RatingController(RestaurantDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RateRestaurant([FromForm] RatingEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ratings.Add(new Rating()
            {
                Score = model.Score,
                RestaurantID = model.RestaurantID
            });
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}