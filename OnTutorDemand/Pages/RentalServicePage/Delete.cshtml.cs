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
    public class DeleteModel : PageModel
    {
        private IRentalServiceRepository _serviceRepository;
        private ITutorRepository _tutorRepository;
        public DeleteModel()
        {
            _serviceRepository = new RentalServiceRepository();
            _tutorRepository = new TutorRepository();   
        }

        [BindProperty]
        public RentalService RentalService { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["TutorId"] = new SelectList(_tutorRepository.GetAllTutors(), "Id", "Description");
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || !userRole.Equals("Tutor"))
            {
                return RedirectToPage("/Authenticate/Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var rentalservice = _serviceRepository.GetRentalServiceById(id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rentalservice = _serviceRepository.GetRentalServiceById(RentalService.Id);

            if (rentalservice != null)
            {
                RentalService = rentalservice;
                _serviceRepository.DeleteRentalService(id);

            }

            return RedirectToPage("/RentalServicePage/RentalServiceIndexForTurtor");
        }
    }
}
