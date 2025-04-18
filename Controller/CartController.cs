using Microsoft.AspNetCore.Mvc;
using CodexGoddess.Data;
using CodexGoddess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodexGoddess.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class CartController : ControllerBase
    {

        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _uManager;

        public CartController(ApplicationDbContext context, UserManager<IdentityUser> uManager)
        {
            _context = context;
            _uManager = uManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] int carId)
        {
            var userId = _uManager.GetUserId(User);

            var item = _context.Carts.Where(x => x.User.Id == userId && x.Cars.Id == carId).FirstOrDefault();
           
            if (item == null)
            {
                item = new Cart();

                item.User = await _uManager.GetUserAsync(User);
                item.Cars = _context.Cars.Where(x => x.Id == carId).FirstOrDefault();
                item.Quantity = 1;
                _context.Add(item);
                _context.SaveChanges();
                return Ok();
            }
            item.Quantity++;
            _context.Carts.Update(item);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("remove", Name = "remove")]
        public async Task<IActionResult> Remove([FromBody] int carId)
        {
            var userId = _uManager.GetUserId(User);

            var item = _context.Carts.Where(x => x.User.Id == userId && x.Cars.Id == carId).FirstOrDefault();
       
            if(item == null) { return BadRequest(); 
            }

            if(item.Quantity <= 1) _context.Carts.Remove(item);
            else
            {
                item.Quantity--;
                _context.Carts.Update(item);
            }
            _context.SaveChanges();
            return Ok();
        }

            [HttpGet]
        public List<Cart> GetCarts()
        {
            var userId = _uManager.GetUserId(User);
            return _context.Carts.Include(x => x.Cars).Where(x  => x.User.Id == userId).ToList();
        }
    }
}