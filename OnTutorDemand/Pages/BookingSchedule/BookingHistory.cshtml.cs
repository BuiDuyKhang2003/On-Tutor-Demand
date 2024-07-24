using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnTutorDemand.Pages.BookingSchedule;

public class BookingHistory : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }
}