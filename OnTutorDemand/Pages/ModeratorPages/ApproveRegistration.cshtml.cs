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

namespace OnTutorDemand.Pages.ModeratorPages
{
    public class ApproveRegistrationModel : PageModel
    {
        private readonly ITutorRegistrationRepository tutorRegistrationRepository;

        public ApproveRegistrationModel(ITutorRegistrationRepository tutorRegistrationRepository)
        {
            this.tutorRegistrationRepository = tutorRegistrationRepository;
        }

        [BindProperty]
        public TutorRegistration TutorRegistration { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorregistration = await tutorRegistrationRepository.GetTutorRegistrationById((int)id);
            if (tutorregistration == null)
            {
                return NotFound();
            }
            TutorRegistration = tutorregistration;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            tutorRegistrationRepository.UpdateTutorRegistration(TutorRegistration);

            return RedirectToPage("./TutorRegistrationPage");
        }
    }
}
