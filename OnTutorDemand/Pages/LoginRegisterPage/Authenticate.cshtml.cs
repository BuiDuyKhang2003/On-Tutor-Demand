using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnTutorDemand.Dto;
using Repository.RepositoryInterface;

namespace OnTutorDemand.Pages
{
    public class AuthenticateModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITutorRegistrationRepository registrationRepository;

        public AuthenticateModel(IAccountRepository accountRepository, ITutorRegistrationRepository registrationRepository)
        {
            this.accountRepository = accountRepository;
            this.registrationRepository = registrationRepository;
        }

        [BindProperty]
        public string EmailLogin { get; set; }

        [BindProperty]
        public string PasswordLogin { get; set; }

        [BindProperty]
        public RegisterInputModel RegisterModel { get; set; }


        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPostLogin()
        {
            //ModelState.Remove(nameof(RegisterModel));
            foreach (var property in typeof(RegisterInputModel).GetProperties())
            {
                ModelState.Remove(property.Name);
            }
            if (ModelState.IsValid)
            {
                var user = accountRepository.GetAccountByEmail(EmailLogin);

                if (user.Password != null)
                {
                    if (user.Password.Equals(PasswordLogin))
                    {
                        HttpContext.Session.SetString("UserEmail", user.Email);
                        HttpContext.Session.SetString("UserRole", user.Role);

                        if (user.Role.Equals("Admin"))
                        {
                            return RedirectToPage("AdminPages/AdminPage");
                        }
                        if (user.Role.Equals("Moderator"))
                        {
                            return RedirectToPage("ModeratorPages/ModeratorPage");
                        }
                        if (user.Role.Equals("Tutor"))
                        {
                            return RedirectToPage("/TutorPages/TutorPage");
                        }
                        return  RedirectToPage("/RentalServicePage/RentalServiceHomePage");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRegister()
        {
            ModelState.Remove("EmailLogin");
            ModelState.Remove("PasswordLogin");
            if (RegisterModel.Role == "user")
            {
                ModelState.Remove("Experience");
                ModelState.Remove("Education");
                ModelState.Remove("Description");
            }

            if (ModelState.IsValid)
            {
                var user = new Account
                {
                    Email = RegisterModel.Email,
                    Password = RegisterModel.Password,
                    Role = RegisterModel.Role,
                    FullName = RegisterModel.Name,
                    DateOfBirth = RegisterModel.Birthdate,
                    Phone = RegisterModel.Phone,
                    Gender = RegisterModel.Gender,
                    Address = RegisterModel.Address,
                    IsActive = true
                };

                if (RegisterModel.Role == "teacher")
                {
                    var tutorRegistration = new TutorRegistration
                    {
                        Experience = RegisterModel.Experience,
                        Education = RegisterModel.Education,
                        Description = RegisterModel.Description
                    };
                    registrationRepository.AddTutorRegistration(tutorRegistration);
                }

                accountRepository.AddAccount(user);
            }

            return Page();
        }
    }
}
