using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.RepositoryInterface;
using System.Threading.Tasks;

namespace OnTutorDemand.Pages.Authenticate
{
    public class LoginPageModel : PageModel
    {
        private readonly IAccountRepository accountRepository;

        public LoginPageModel(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [BindProperty]
        public string EmailLogin { get; set; }

        [BindProperty]
        public string PasswordLogin { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostLogin()
        {
            if (ModelState.IsValid)
            {
                var user = await accountRepository.GetAccountByEmail(EmailLogin);

                if (user != null && user.Password.Equals(PasswordLogin))
                {
                    if (!user.IsActive)
                    {
                        TempData["LoginMessage"] = "Tài khoản của bạn đã bị vô hiệu hóa.";
                        return Page();
                    }

                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserRole", user.Role);

                    return user.Role switch
                    {
                        "Admin" => RedirectToPage("/AdminPages/AdminPage"),
                        "Moderator" => RedirectToPage("/ModeratorPages/ModeratorPage"),
                        "Tutor" => RedirectToPage("/RentalServicePage/RentalServiceIndex"),
                        _ => RedirectToPage("/OnDemandTutorHomePage")
                    };
                }

                TempData["LoginMessage"] = "Email hoặc Mật khẩu không chính xác";
            }

            return Page();
        }
    }
}