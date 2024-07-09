using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnTutorDemand.Pages.Authenticate;

public class Logout : PageModel
{
    public IActionResult OnGet()
    {
        HttpContext.Session.Clear();
        TempData["LogoutMessage"] = "You have been logged out.";
        return RedirectToPage("/Authenticate/LoginRegisterPage");
    }
}