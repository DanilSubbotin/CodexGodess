using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodexGoddess.Data;
using CodexGoddess.Models;

namespace CodexGoddess.Pages
{
    public class AddCarModel : PageModel
    {
        private readonly CodexGoddess.Data.ApplicationDbContext _context;

        public AddCarModel(CodexGoddess.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Cars Cars { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cars.Add(Cars);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
