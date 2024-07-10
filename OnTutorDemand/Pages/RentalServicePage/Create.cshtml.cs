using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using DataAccessLayer;
using Repository.RepositoryInterface;
using Repository;
using System.Drawing.Printing;

namespace OnTutorDemand.Pages.RentalServicePage
{
    public class CreateModel : PageModel
    {
        private IRentalServiceRepository _serviceRepository;
        private ITutorRepository _tutorRepository;
        public CreateModel()
        {
            _tutorRepository = new TutorRepository();
            _serviceRepository = new RentalServiceRepository();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || !userRole.Equals("Tutor"))
            {
                return RedirectToPage("/Authenticate/LoginRegisterPage");
            }

            return Page();
        }

        [BindProperty]
        public RentalService RentalService { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }


            _serviceRepository.AddRentalService(RentalService);


            return RedirectToPage("/RentalServicePage/RentalServiceHomePage");
        }
    }
}
