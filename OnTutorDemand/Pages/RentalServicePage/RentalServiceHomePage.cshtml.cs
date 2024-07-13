using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.RepositoryInterface;
using Repository;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using OnTutorDemand.Pages.PaginatedListFeature;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public PaginatedList<RentalService> RentalServices { get; set; }
        public PaginatedList<Schedule> Schedules { get; set; }
        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            ViewData["RentalServiceId"] = new SelectList(_serviceRepository.GetAllRentalServices(), "Id", "Description");
           
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || (!userRole.Equals("Tutor") && !userRole.Equals("User")))
            {
                RedirectToPage("/Authenticate/LoginRegisterPage");
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
            IQueryable<RentalService> serviceIQ = _serviceRepository.GetRentalServicesByQuery();
            IQueryable<Schedule> scheduleIQ = _scheduleRepository.GetScheduleServicesByQuery();
            if (!String.IsNullOrEmpty(searchString))
            {
                serviceIQ = serviceIQ.Where(s => s.Description.ToUpper().Contains(searchString));
                scheduleIQ = scheduleIQ.Where(sc => sc.DayOfWeek.ToUpper().Contains(searchString)
                || sc.StartTime.ToString().Contains(searchString)
                || sc.EndTime.ToString().Contains(searchString));
            }

            //RentalService = _serviceRepository.GetAllRentalServices().ToList();
            //Schedule = _scheduleRepository.GetAllSchedules().ToList();
            switch (sortOrder)
            {
                case "Description_desc":
                    serviceIQ = serviceIQ.OrderByDescending(s => s.Description);
                    break;
                default:
                    serviceIQ = serviceIQ.OrderBy(s => s.Description);
                    break;
            }
            var pageSize = _configuration.GetValue("PageSize", 4);
            RentalServices = await PaginatedList<RentalService>.CreateAsync(
                serviceIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            Schedules = await PaginatedList<Schedule>.CreateAsync(
                scheduleIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        [BindProperty]
        //public IList<Schedule> Schedule { get; set; } = default!;
        public Schedule Schedule { get; set; } = default!;
        //public RentalService RentalService { get; set; } = default!;
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
