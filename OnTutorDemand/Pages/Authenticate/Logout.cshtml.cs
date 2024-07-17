using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnTutorDemand.Pages.Authenticate;

public class Logout : PageModel
{
    public IActionResult OnGet()
    {
        HttpContext.Session.Clear();
        TempData["LogoutMessage"] = "Bạn đã thoát khỏi phiên đăng nhập";
        return RedirectToPage("/Authenticate/Login");
    }
}