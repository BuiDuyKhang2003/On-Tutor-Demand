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

namespace OnTutorDemand.Pages.TutorPages
{
    public class DetailsModel : PageModel
    {
        private ITutorRepository _tutorRepository;

        public DetailsModel()
        {
            _tutorRepository = new TutorRepository();
        }

        public Tutor Tutor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = _tutorRepository.GetTutorById(id);
            if (tutor == null)
            {
                return NotFound();
            }
            else
            {
                Tutor = tutor;
            }
            return Page();
        }
    }
}
