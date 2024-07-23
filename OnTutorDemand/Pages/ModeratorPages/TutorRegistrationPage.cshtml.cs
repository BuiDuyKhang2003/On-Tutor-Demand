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

namespace OnTutorDemand.Pages.ModeratorPages
{
    public class TutorRegistrationPageModel : PageModel
    {
        private readonly ITutorRegistrationRepository tutorRegistrationRepository;

        public TutorRegistrationPageModel(ITutorRegistrationRepository tutorRegistrationRepository)
        {
            this.tutorRegistrationRepository = tutorRegistrationRepository;
        }

        public IList<TutorRegistration> TutorRegistration { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (tutorRegistrationRepository.GetAllTutorRegistrations != null)
            {
                var tutorRegistrations = await tutorRegistrationRepository.GetAllTutorRegistrations();
                TutorRegistration = tutorRegistrations.OrderByDescending(t => t.ApplicationDate).ToList();
            }
        }
    }
}
