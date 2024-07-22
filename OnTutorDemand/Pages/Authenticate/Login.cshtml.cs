﻿using BusinessObject;
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
                            return RedirectToPage("/RentalServicePage/RentalServiceIndex");
                        }
                        return RedirectToPage("/OnDemandTutorHomePage");
                    }
                }
                TempData["LoginMessage"] = "Email hoặc Mật khẩu không chính xác";
            }

            return Page();
        }
    }
}
