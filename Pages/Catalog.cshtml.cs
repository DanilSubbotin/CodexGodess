using CodexGoddess.Data;
using CodexGoddess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodexGoddess.Pages
{
    public class CatalogModel : PageModel
    {
        public List<Cars> cars;

        private ApplicationDbContext _context;

        public CatalogModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            cars = _context.Cars.ToList();
        }
    }
}
