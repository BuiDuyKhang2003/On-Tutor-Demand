using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccessLayer;

namespace OnTutorDemand.Pages.RentalServicePage
{
    public class IndexModel : PageModel
    {
        private readonly DataAccessLayer.AppDbContext _context;

        public IndexModel(DataAccessLayer.AppDbContext context)
        {
            _context = context;
        }

        public IList<RentalService> RentalService { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || !userRole.Equals("Tutor"))
            {
                return RedirectToPage("/Authenticate/LoginRegisterPage");
            }

            if (_context.RentalServices != null)
            {
                RentalService = await _context.RentalServices
                    .Include(r => r.Tutor).ToListAsync();
            }

            return Page();
        }
    }
}
