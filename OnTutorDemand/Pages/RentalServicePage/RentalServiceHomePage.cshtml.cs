using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.RepositoryInterface;
using Repository;
using BusinessObject;

namespace OnTutorDemand.Pages.RentalServicePage
{
    public class RentalServiceHomePageModel : PageModel
    {
        private IRentalServiceRepository _serviceRepository;
        
        public RentalServiceHomePageModel()
        {

            _serviceRepository = new RentalServiceRepository();
        }

        public IList<RentalService> RentalService { get; set; } = default!;

        public IActionResult OnGet()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || (!userRole.Equals("Tutor") && !userRole.Equals("User")))
            {
                return RedirectToPage("/Authenticate/LoginRegisterPage");
            }

            RentalService = _serviceRepository.GetAllRentalServices().ToList();

            return Page();
        }
    }
}
