using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnTutorDemand.Dto;
using Repository.RepositoryInterface;

namespace OnTutorDemand.Pages.Authenticate
{
    public class LoginRegisterModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITutorRegistrationRepository registrationRepository;

        public LoginRegisterModel(IAccountRepository accountRepository, ITutorRegistrationRepository registrationRepository)
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

        public async Task<IActionResult> OnPostLogin()
        {
            foreach (var property in typeof(RegisterInputModel).GetProperties())
            {
                ModelState.Remove(property.Name);
            }
            if (ModelState.IsValid)
            {
                var user = await accountRepository.GetAccountByEmail(EmailLogin);

                if (user.Password != null)
                {
                    if (user.Password.Equals(PasswordLogin))
                    {
                        HttpContext.Session.SetString("UserEmail", user.Email);
                        HttpContext.Session.SetString("UserRole", user.Role);

                        if (user.Role.Equals("Admin"))
                        {
                            return RedirectToPage("/AdminPages/AdminPage");
                        }
                        if (user.Role.Equals("Moderator"))
                        {
                            return RedirectToPage("/ModeratorPages/ModeratorPage");
                        }
                        if (user.Role.Equals("Tutor"))
                        {
                            return RedirectToPage("/RentalServicePage/RentalServiceHomePage");
                        }
                        return  RedirectToPage("/TutorPages/TutorPage");
                    }
                }
                TempData["LoginMessage"] = "Email hoặc Mật khẩu không chính xác";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRegister()
        {
            ModelState.Remove("EmailLogin");
            ModelState.Remove("PasswordLogin");
            if (RegisterModel.Role == "user")
            {
                ModelState.Remove("RegisterModel.Experience");
                ModelState.Remove("RegisterModel.Education");
                ModelState.Remove("RegisterModel.Description");
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

                if (RegisterModel.Role == "user")
                {
                    user.Role = "User";
                    accountRepository.AddAccount(user);
                    return RedirectToPage("/Authenticate/LoginRegisterPage");
                }

                if (RegisterModel.Role == "teacher")
                {
                    var tutorRegistration = new TutorRegistration
                    {
                        Email = RegisterModel.Email,
                        Password = RegisterModel.Password,
                        FullName = RegisterModel.Name,
                        DateOfBirth = RegisterModel.Birthdate,
                        Phone = RegisterModel.Phone,
                        Gender = RegisterModel.Gender,
                        Address = RegisterModel.Address,
                        Experience = RegisterModel.Experience,
                        Education = RegisterModel.Education,
                        Status = "Waiting",
                        Description = RegisterModel.Description
                    };
                    registrationRepository.AddTutorRegistration(tutorRegistration);
                    return RedirectToPage("/Authenticate/LoginRegisterPage");
                }
            }

            return Page();
        }
    }
}
