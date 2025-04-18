using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CodexGoddess.Models;
using CodexGoddess.Data;

namespace CodexGoddess.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class CatalogController : ControllerBase
    {

        private ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public List<Cars> Get(string query = "")
        {
            return _context.Cars.Where(s => s.Name.Contains(query)).ToList();
        }
    }
}
