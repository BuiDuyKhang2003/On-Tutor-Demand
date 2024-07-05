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

namespace OnTutorDemand.Pages.RentalServicePage
{
    public class DetailsModel : PageModel
    {
        private IRentalServiceRepository _serviceRepository;

        public DetailsModel()
        {
            _serviceRepository = new RentalServiceRepository();
        }

        public RentalService RentalService { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalservice = _serviceRepository.GetRentalServiceById(id);
            if (rentalservice == null)
            {
                return NotFound();
            }
            else
            {
                RentalService = rentalservice;
            }
            return Page();
        }
    }
}
