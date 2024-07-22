
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnTutorDemand.Pages.PaginatedListFeature;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.RepositoryInterface;
using Repository;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace OnTutorDemand.Pages.RentalServicePage
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
        public List<RentalService> RentalServices { get; set; } = default!;
        public PaginatedList<Schedule> Schedules { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            ViewData["RentalServiceId"] = new SelectList(_serviceRepository.GetAllRentalServices(), "Id", "Description");
            ViewData["DaysOfWeek"] = new SelectList(new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" });

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || (!userRole.Equals("Tutor") && !userRole.Equals("User")))
            {
                RedirectToPage("/Authenticate/Login");
            }

            CurrentSort = sortOrder;
            desSort = String.IsNullOrEmpty(sortOrder) ? "Description_desc" : "";
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
            if (!String.IsNullOrEmpty(searchString))
            {
               
                scheduleIQ = scheduleIQ.Where(sc => sc.DayOfWeek.ToUpper().Contains(searchString)
                || sc.StartTime.ToString().Contains(searchString)
                || sc.EndTime.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
               
            }
            var pageSize = _configuration.GetValue("PageSize", 4);
            Schedules = await PaginatedList<Schedule>.CreateAsync(
                scheduleIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        [BindProperty]
        public Schedule Schedule { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _scheduleRepository.AddSchedule(Schedule);
            return RedirectToPage("/RentalServicePage/RentalServiceHomePage");
        }
    }
}
