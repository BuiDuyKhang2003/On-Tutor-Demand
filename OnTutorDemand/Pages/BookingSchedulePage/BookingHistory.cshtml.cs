﻿using BusinessObject;
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