using Microsoft.AspNetCore.Mvc;
using CodexGoddess.Data;
using CodexGoddess.Models;

namespace CodexGoddess.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : ControllerBase
    {

        private ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("addnewcar", Name = "addnewcar")]
        public async Task<IActionResult> AddNewCar(Cars data) 
        {
            _context.Cars.Add(data);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}