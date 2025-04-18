using CodexGoddess.Data;
using CodexGoddess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodexGoddess.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _uManager;

        public OrderController(ApplicationDbContext context, UserManager<IdentityUser> uManager)
        {
            _context = context;
            _uManager = uManager;
        }

        [HttpPost]

        public async Task<IActionResult> Post()
        {
            var order = new Order();

            order.User = await _uManager.GetUserAsync(User);
            order.Address = "";
            order.Status = "Создана";

            _context.Order.Add(order);
            _context.SaveChanges();
            
            var userId = _uManager.GetUserId(User);

            List<OrderItem> items = new List<OrderItem>();
            
            List<Cart> cartdata = _context.Carts.Include(x => x.Cars).Where(x => x.User.Id == userId).ToList();
        
            foreach (var cart in  cartdata)
            {
                items.Add(new OrderItem { Order = order, Cars = cart.Cars });
            }

            _context.OrderItem.AddRange(items);
            _context.SaveChanges();

            _context.Carts.RemoveRange(_context.Carts.Where(x => x.User.Id == userId));
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public List<Order> Get() 
        {
            return _context.Order.Include(x => x.User).ToList();
        }

        [HttpPut]

        public void Put([FromBody] int orderid)
        {
            var item = _context.Order.Where(x => x.Id == orderid).FirstOrDefault();
            item.Status = "Подтверждена";
            _context.Order.Update(item);
            _context.SaveChanges();
        }
    }
}
