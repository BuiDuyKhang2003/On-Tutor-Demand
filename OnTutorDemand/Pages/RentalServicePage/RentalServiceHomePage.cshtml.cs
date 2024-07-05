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

        public async Task<IActionResult> OnGetAsync()
        {

            RentalService = _serviceRepository.GetAllRentalServices().ToList();

            return Page();
        }
    }
}
