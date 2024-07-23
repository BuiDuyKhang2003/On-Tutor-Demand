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
using Microsoft.AspNetCore.Mvc.Rendering;
using OnTutorDemand.Pages.PaginatedListFeature;

namespace OnTutorDemand.Pages.RentalServicePage
{
    public class RentalServiceIndexForTutorModel : PageModel
    {
        private IConfiguration _configuration;
        private IRentalServiceRepository rentalServiceRepository;
        private ITutorRepository _tutorRepository;
        public RentalServiceIndexForTutorModel(IConfiguration configuration)
        {
            rentalServiceRepository = new RentalServiceRepository();
            _tutorRepository = new TutorRepository();
            _configuration = configuration;
        }
        public string desSort { get; set; }
        public string CurrentSort { get; set; }
        
        public PaginatedList<RentalService> RentalService { get; set; }

        public async Task OnGetAsync(string sortOrder, int? pageIndex)
        {
            
            ViewData["TutorId"] = new SelectList(_tutorRepository.GetAllTutors(), "Id", "FullName");
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || (!userRole.Equals("Tutor") && !userRole.Equals("User")))
            {
                RedirectToPage("/Authenticate/Login");
            }

            CurrentSort = sortOrder;
            desSort = String.IsNullOrEmpty(sortOrder) ? "Description_desc" : "";
           
           
            IQueryable<RentalService> serviceIQ = rentalServiceRepository.GetRentalServicesByQuery();
           
            
            switch (sortOrder) {
                case "Description_desc":
                    serviceIQ = serviceIQ.OrderByDescending(s => s.Description);
                    break;
                default:
                    serviceIQ = serviceIQ.OrderBy(s => s.Description);
                    break;
            }
             
                var pageSize = _configuration.GetValue("PageSize", 8);
                RentalService = await PaginatedList<RentalService>.CreateAsync(
                    serviceIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
    }
}
