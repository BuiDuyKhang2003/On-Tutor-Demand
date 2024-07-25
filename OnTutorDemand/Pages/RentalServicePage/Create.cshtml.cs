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
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace OnTutorDemand.Pages.RentalServicePage
{
    public class CreateModel : PageModel
    {
        private IRentalServiceRepository _serviceRepository;
        private ITutorRepository _tutorRepository;
        private IAccountRepository accountRepository;
        public CreateModel(IAccountRepository accountRepository)
        {
            _tutorRepository = new TutorRepository();
            _serviceRepository = new RentalServiceRepository();
            this.accountRepository = accountRepository;
        }

        public IActionResult OnGet()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || !userRole.Equals("Tutor"))
            {
                return RedirectToPage("/Authenticate/Login");
            }

            return Page();
        }

        [BindProperty]
        public RentalService RentalService { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var currentAccount = await accountRepository.GetAccountByEmail(userEmail);

            ModelState.Remove("RentalService.TutorId");
            ModelState.Remove("RentalService.Tutor");
            ModelState.Remove("RentalService.CreatedDate");
            if (!ModelState.IsValid)
            {
                return Page();

            }

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://worldtimeapi.org/api/timezone/Etc/UTC");
            var jsonDocument = JsonDocument.Parse(response);
            var datetimeString = jsonDocument.RootElement.GetProperty("datetime").GetString();
            var vietNamTime = DateTime.SpecifyKind(DateTime.Parse(datetimeString), DateTimeKind.Utc);

            RentalService.CreatedDate = vietNamTime;
            RentalService.TutorId = currentAccount.Tutor.Id;

            await _serviceRepository.AddRentalService(RentalService);

            return RedirectToPage("/RentalServicePage/RentalServiceIndexForTurtor");
        }
    }
}
