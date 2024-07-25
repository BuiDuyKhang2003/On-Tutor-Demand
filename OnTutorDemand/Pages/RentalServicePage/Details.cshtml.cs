using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccessLayer;
using Repository.RepositoryInterface;
using Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnTutorDemand.Pages.RentalServicePage
{
    public class DetailsModel : PageModel
    {
        private IRentalServiceRepository _serviceRepository;
        private ITutorRepository _tutorRepository;
        public DetailsModel()
        {
            _serviceRepository = new RentalServiceRepository();
            _tutorRepository = new TutorRepository();          
        }

        public RentalService RentalService { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["TutorId"] = new SelectList(_tutorRepository.GetAllTutors(), "Id", "FullName");
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || !userRole.Equals("Tutor"))
            {
                return RedirectToPage("/Authenticate/Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var rentalservice = await _serviceRepository.GetRentalServiceById(id);
            if (rentalservice == null)
            {
                return NotFound();
            }
            else
            {
                RentalService = rentalservice;
            }
            return Page();
        }
    }
}
