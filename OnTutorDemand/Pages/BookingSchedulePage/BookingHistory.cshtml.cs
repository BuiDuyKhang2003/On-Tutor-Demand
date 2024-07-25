using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.RepositoryInterface;

namespace OnTutorDemand.Pages.BookingSchedulePage;

public class BookingHistory : PageModel
{
    private readonly IBookingScheduleRepository bookingScheduleRepository;

    public BookingHistory(IBookingScheduleRepository bookingScheduleRepository)
    {
        this.bookingScheduleRepository = bookingScheduleRepository;
    }

    public IActionResult OnGet()
    {
        return Page();
    }
}