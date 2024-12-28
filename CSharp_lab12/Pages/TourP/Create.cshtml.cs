using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TourClass;

namespace CSharp_lab12.Pages.TourP
{
    public class CreateModel : PageModel
    {
        private readonly TourContext _context;

        public CreateModel(TourContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Tour Tour { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tours.Add(Tour);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
