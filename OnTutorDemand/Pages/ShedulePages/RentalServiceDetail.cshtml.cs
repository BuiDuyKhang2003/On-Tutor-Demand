
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnTutorDemand.Pages.PaginatedListFeature;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.RepositoryInterface;
using Repository;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace OnTutorDemand.Pages.ShedulePages
{
    public class RentalServiceHomePageModel : PageModel
    {
        private IRentalServiceRepository _serviceRepository;
        private IConfiguration _configuration;
        private IScheduleRepository _scheduleRepository;

        public RentalServiceHomePageModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceRepository = new RentalServiceRepository();
            _scheduleRepository = new ScheduleRepository();
        }

        public string desSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }
        public RentalService RentalService { get; set; } = default!;
        public PaginatedList<Schedule> Schedules { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            var rentalservice = await _serviceRepository.GetRentalServiceById(id);
            if (rentalservice == null)
            {
                return NotFound();
            }
            else
            {
                RentalService = rentalservice;
            }
            ViewData["DaysOfWeek"] = new SelectList(new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" });
            ViewData["RentalServiceId"] = new SelectList(_serviceRepository.GetAllRentalServices(), "Id", "Description");
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || !userRole.Equals("Tutor"))
            {
                RedirectToPage("/Authenticate/Login");
            }

            CurrentSort = sortOrder;
            desSort = string.IsNullOrEmpty(sortOrder) ? "Description_desc" : "";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            IQueryable<Schedule> scheduleIQ = _scheduleRepository.GetScheduleServicesByQuery();
            if (!string.IsNullOrEmpty(searchString))
            {

                scheduleIQ = scheduleIQ.Where(sc => sc.DayOfWeek.ToUpper().Contains(searchString)
                || sc.StartTime.ToString().Contains(searchString)
                || sc.EndTime.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {

            }
            var pageSize = _configuration.GetValue("PageSize", 8);
            Schedules = await PaginatedList<Schedule>.CreateAsync(
                scheduleIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            return Page();
        }


        [BindProperty]
        public Schedule Schedule { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _scheduleRepository.AddScheduleAsync(Schedule);
            return RedirectToPage("./RentalServiceDetail");
        }
    }
}

