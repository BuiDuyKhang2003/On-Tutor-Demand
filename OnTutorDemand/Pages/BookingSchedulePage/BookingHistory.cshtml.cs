using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.RepositoryInterface;

namespace OnTutorDemand.Pages.BookingSchedulePage;

public class BookingHistory : PageModel
{
        private readonly IBookingScheduleRepository bookingScheduleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountRepository _accountRepository;

        public BookingHistory(IBookingScheduleRepository bookingScheduleRepository, IHttpContextAccessor httpContextAccessor, IAccountRepository accountRepository)
        {
            this.bookingScheduleRepository = bookingScheduleRepository;
            _httpContextAccessor = httpContextAccessor;
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public int UserId { get; set; }

        public List<BookingSchedule> BookingHistoryList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userEmail = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");
            var currentAccount = await _accountRepository.GetAccountByEmail(userEmail);
            if (currentAccount == null || string.IsNullOrEmpty(currentAccount.Email))
            {
                return RedirectToPage("/Authenticate/Login");
            }

            UserId = currentAccount.Id;
            BookingHistoryList = (await bookingScheduleRepository.GetBookingHistoryAsync(UserId)).ToList();
            return Page();
        }
    
    public async Task<IActionResult> OnPostCancelBookingAsync(int bookingId)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole == null)
        {
            RedirectToPage("/Authenticate/Login");
        }

        var success = await bookingScheduleRepository.CancelBookingAsync(bookingId);
        if (!success)
            return BadRequest();

        return RedirectToPage();
    }
}
