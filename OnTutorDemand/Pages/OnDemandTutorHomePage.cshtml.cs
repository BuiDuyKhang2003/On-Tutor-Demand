using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnTutorDemand.Pages;

public class OnDemandTutorHomePage : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }
}