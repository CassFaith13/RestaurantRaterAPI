using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterAPI.Models;

namespace RestaurantRaterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private RestaurantDBContext _context;
        public RestaurantController(RestaurantDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostRestaurant([FromForm] RestaurantEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Restaurants.Add(new Restaurant()
            {
                Name = model.Name,
                Location = model.Location
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _context.Restaurants.Include(r => r.Ratings).ToListAsync();

            List<RestaurantListItem> restaurantList = restaurants.Select(r => new RestaurantListItem()
            {
                ID = r.ID,
                Name = r.Name,
                Location = r.Location,
                AverageScore = r.AverageRating
            }).ToList();

            return Ok(restaurants);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetRestaurantByID(int id)
        {
            var restaurant = await _context.Restaurants.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.ID == id);

            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromForm] RestaurantEdit model, int id)
        {
            var oldRestaurant = await _context.Restaurants.FindAsync(id);

            if (oldRestaurant == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!string.IsNullOrEmpty(model.Name))
            {
                oldRestaurant.Name = model.Name;
            }
            if (!string.IsNullOrEmpty(model.Location))
            {
                oldRestaurant.Location = model.Location;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromForm] int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}