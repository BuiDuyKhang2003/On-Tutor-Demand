using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccessLayer;
using Repository.RepositoryInterface;
using Repository;

namespace OnTutorDemand.Pages.ShedulePages
{
    public class DeleteModel : PageModel
    {
        private IScheduleRepository scheduleRepository;

        public DeleteModel()
        {
           scheduleRepository = new ScheduleRepository();
        }

        [BindProperty]
      public Schedule Schedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || !userRole.Equals("Tutor"))
            {
                RedirectToPage("/Authenticate/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await scheduleRepository.GetScheduleById(id);

            if (schedule == null)
            {
                return NotFound();
            }
            else 
            {
                Schedule = schedule;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var schedule = await scheduleRepository.GetScheduleById(id);

            if (schedule != null)
            {
                Schedule = schedule;
                scheduleRepository.DeleteSchedule(id);
            }

            return RedirectToPage("./RentalServiceDetail");
        }
    }
}
