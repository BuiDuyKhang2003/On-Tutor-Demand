using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnTutorDemand.Dto;
using Repository.RepositoryInterface;
using System.Threading.Tasks;

namespace OnTutorDemand.Pages.Authenticate
{
    public class RegisterTutorModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITutorRegistrationRepository registrationRepository;

        public RegisterTutorModel(IAccountRepository accountRepository, ITutorRegistrationRepository registrationRepository)
        {
            this.accountRepository = accountRepository;
            this.registrationRepository = registrationRepository;
        }

        [BindProperty]
        public RegisterInputModel RegisterModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostRegister()
        {
            ModelState.Remove("EmailLogin");
            ModelState.Remove("PasswordLogin");

            if (RegisterModel.Role != "tutor")
            {
                ModelState.AddModelError(string.Empty, "Invalid role for this page.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                var tutorRegistration = new TutorRegistration
                {
                    FullName = RegisterModel.Name,
                    Gender = RegisterModel.Gender,
                    Email = RegisterModel.Email,
                    Password = RegisterModel.Password,
                    Address = RegisterModel.Address,
                    DateOfBirth = RegisterModel.Birthdate,
                    Phone = RegisterModel.Phone,
                    Experience = RegisterModel.Experience,
                    Education = RegisterModel.Education,
                    Description = RegisterModel.Description,
                    IsApproved = false
                };

                registrationRepository.AddTutorRegistration(tutorRegistration);
                return RedirectToPage("/Authenticate/Login");
            }

            return Page();
        }
    }
}
