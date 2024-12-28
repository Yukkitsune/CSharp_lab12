using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TourClass;

namespace CSharp_lab12.Pages.ClientP
{
    public class IndexModel : PageModel
    {
        private readonly TourContext _context;

        public IndexModel(TourContext context)
        {
            _context = context;
        }

        public IList<Client> Client { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Client = await _context.Clients.ToListAsync();
        }
    }
}
