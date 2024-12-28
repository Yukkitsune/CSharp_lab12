using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TourClass;

namespace CSharp_lab12.Pages.TourP
{
    public class DeleteModel : PageModel
    {
        private readonly TourContext _context;

        public DeleteModel(TourContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tour Tour { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours.FirstOrDefaultAsync(m => m.TourId == id);

            if (tour == null)
            {
                return NotFound();
            }
            else
            {
                Tour = tour;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours.FindAsync(id);
            if (tour != null)
            {
                Tour = tour;
                _context.Tours.Remove(Tour);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
