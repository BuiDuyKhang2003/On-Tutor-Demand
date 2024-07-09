using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccessLayer;
using Repository.RepositoryInterface;
using Repository;

namespace OnTutorDemand.Pages.RentalServicePage
{
    public class EditModel : PageModel
    {
        private IRentalServiceRepository _serviceRepository;
        private ITutorRepository _tutorRepository;
        public EditModel()
        {
            _tutorRepository = new TutorRepository();
            _serviceRepository = new RentalServiceRepository();
        }

        [BindProperty]
        public RentalService RentalService { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || !userRole.Equals("Tutor"))
            {
                return RedirectToPage("LoginRegisterPage/Authenticate");
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
            RentalService = rentalservice;
            //ViewData["TutorId"] = new SelectList(_tutorRepository.GetAllTutors(), "Id", "description");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }



            try
            {
                _serviceRepository.UpdateRentalService(RentalService);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception();
            }

            return RedirectToPage("/RentalServicePage/RentalServiceHomePage");
        }
    }
}
