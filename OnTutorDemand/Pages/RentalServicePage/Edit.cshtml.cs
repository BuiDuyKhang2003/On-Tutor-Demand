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
using System.Text.Json;

namespace OnTutorDemand.Pages.RentalServicePage
{
    public class EditModel : PageModel
    {
        private IRentalServiceRepository _serviceRepository;
        private ITutorRepository _tutorRepository;
        private IAccountRepository accountRepository;
        public EditModel(IAccountRepository accountRepository)
        {
            _tutorRepository = new TutorRepository();
            _serviceRepository = new RentalServiceRepository();
            this.accountRepository = accountRepository;
        }
        [BindProperty]
        public RentalService RentalService { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || !userRole.Equals("Tutor"))
            {
                RedirectToPage("/Authenticate/Login");
            }
            if (id == null)
            {
                return NotFound();
            }
            var retalService = await _serviceRepository.GetRentalServiceById(id);
            if (retalService == null)
            {
                return NotFound();
            }
            return Page();
        }

        

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var currentAccount = await accountRepository.GetAccountByEmail(userEmail);
            var retalService = await _serviceRepository.GetRentalServiceById(id);
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
           RentalService.Id = retalService.Id;



            try
            {
              await  _serviceRepository.UpdateRentalService(RentalService);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception();
            }

            return RedirectToPage("/RentalServicePage/RentalServiceIndexForTurtor");
        }
    }
}
