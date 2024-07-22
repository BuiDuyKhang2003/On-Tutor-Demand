using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnTutorDemand.Dto;
using Repository.RepositoryInterface;
using System.Threading.Tasks;

namespace OnTutorDemand.Pages.Authenticate
{
    public class RegisterPageModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITutorRegistrationRepository registrationRepository;

        public RegisterPageModel(IAccountRepository accountRepository, ITutorRegistrationRepository registrationRepository)
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

            if (RegisterModel.Role == "student")
            {
                ModelState.Remove("RegisterModel.Experience");
                ModelState.Remove("RegisterModel.Education");
                ModelState.Remove("RegisterModel.Description");
                ModelState.Remove("RegisterModel.SelectedGrades");
                ModelState.Remove("RegisterModel.SelectedSubjects");
                ModelState.Remove("RegisterModel.SelectedAreas");
            }

            if (ModelState.IsValid)
            {
                var user = new Account
                {
                    Email = RegisterModel.Email,
                    Password = RegisterModel.Password,
                    FullName = RegisterModel.Name,
                    DateOfBirth = RegisterModel.Birthdate,
                    Phone = RegisterModel.Phone,
                    Gender = RegisterModel.Gender,
                    Address = RegisterModel.Address,
                    IsActive = true
                };

                if (RegisterModel.Role.Equals("student"))
                {
                    user.Role = "User";
                    await accountRepository.AddAccount(user);
                    return RedirectToPage("/Authenticate/Login");
                }           
            }

            return Page();
        }
    }
}