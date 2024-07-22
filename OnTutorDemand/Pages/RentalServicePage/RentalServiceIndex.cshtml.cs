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
using OnTutorDemand.Pages.PaginatedListFeature;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace OnTutorDemand.Pages.RentalServicePage
{
    public class RentalServiceIndexModel : PageModel
    {
        private IConfiguration _configuration;
        private IRentalServiceRepository _serviceRepository;
        private ITutorRepository _tutorRepository;
        public RentalServiceIndexModel(IConfiguration configuration)
        {
            _serviceRepository = new RentalServiceRepository(); 
            _configuration = configuration;
            _tutorRepository = new TutorRepository();
        }
        public string desSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }
        public PaginatedList<RentalService> RentalServices { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            ViewData["RentalServiceId"] = new SelectList(_serviceRepository.GetAllRentalServices(), "Id", "Description");
            ViewData["TutorId"] = new SelectList(_tutorRepository.GetAllTutors(), "Id", "FullName");
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
            IQueryable<RentalService> serviceIQ = _serviceRepository.GetRentalServicesByQuery();
            if (!String.IsNullOrEmpty(searchString))
            {
                serviceIQ = serviceIQ.Include(t => t.Tutor).ThenInclude(ta => ta.TutorAreas)
                    .ThenInclude(ta => ta.District)
                    .Include(t => t.Tutor).ThenInclude(t => t.TutorGrades)
                    .ThenInclude(tg => tg.Grade)
                    .Include(t => t.Tutor).ThenInclude(t => t.TutorSubjects)
                    .ThenInclude(ts => ts.Subject).Where(s =>
                     s.Tutor.TutorAreas.Any(ta => ta.District.DistrictName.ToUpper().Contains(searchString))
                    || s.Tutor.TutorGrades.Any(ta => ta.Grade.GradeName.ToUpper().Contains(searchString))
                    || s.Tutor.TutorSubjects.Any(ta => ta.Subject.SubjectName.ToUpper().Contains(searchString))

                    );
            }
            switch (sortOrder) {
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
            }
    }
}
