using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.RepositoryInterface;

namespace OnTutorDemand.Pages
{
    public class AuthenticateModel : PageModel
    {
        private readonly IAccountRepository accountRepository;

        public AuthenticateModel(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = accountRepository.GetAccountByEmail(Email);

                if (user.Password != null)
                {
                    if (user.Password.Equals(Password))
                    {
                        HttpContext.Session.SetString("UserEmail", user.Email);
                        HttpContext.Session.SetString("UserRole", user.Role);

                        if (user.Role.Equals("Admin"))
                        {
                            return RedirectToPage("AdminPage");
                        }
                        if (user.Role.Equals("Moderator"))
                        {
                            return RedirectToPage("ModeratorPage");
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
    }
}
