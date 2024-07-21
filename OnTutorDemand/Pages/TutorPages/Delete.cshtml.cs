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
    public class DeleteModel : PageModel
    {
        private ITutorRepository _tutorRepository;

        public DeleteModel()
        {
            _tutorRepository = new TutorRepository();
        }

        [BindProperty]
        public Tutor Tutor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || !userRole.Equals("Tutor"))
            {
                return RedirectToPage("/Authenticate/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            //var tutor = await _context.Tutors.FirstOrDefaultAsync(m => m.Id == id);
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tutor = _tutorRepository.GetTutorById(id);

            if (tutor != null)
            {
                Tutor = tutor;
                _tutorRepository.DeleteTutor(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
