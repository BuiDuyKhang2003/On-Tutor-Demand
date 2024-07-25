using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccessLayer;
using Repository.RepositoryInterface;
using Repository;

namespace OnTutorDemand.Pages.ShedulePages
{
    public class EditModel : PageModel
    {
        private IRentalServiceRepository _serviceRepository;
       
        private IScheduleRepository _scheduleRepository;


        public EditModel()
        {
          _serviceRepository = new RentalServiceRepository();
            _scheduleRepository = new ScheduleRepository();
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

            var schedule = await _scheduleRepository.GetScheduleById(id);
            if (schedule == null)
            {
                return NotFound();
            }
            Schedule = schedule;
            ViewData["RentalServiceId"] = new SelectList(_serviceRepository.GetAllRentalServices(), "Id", "Description");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }



            try
            {
               await _scheduleRepository.UpdateSchedule(Schedule);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception();
            }

            return RedirectToPage("./RentalServiceDetail");
        }

    }
}
