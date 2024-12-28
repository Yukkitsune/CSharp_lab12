using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TourClass;

namespace CSharp_lab12.Pages.BookingP
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstName");
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "Description");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid.");

                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
