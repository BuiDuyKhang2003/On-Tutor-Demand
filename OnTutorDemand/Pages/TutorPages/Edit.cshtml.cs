using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccessLayer;
using Repository.RepositoryInterface;
using Repository;

namespace OnTutorDemand.Pages.TutorPages
{
    public class EditModel : PageModel
    {
        private ITutorRepository _tutorRepository;
        //private IAccountRepository _accountRepository;

        public EditModel()
        {
            _tutorRepository = new TutorRepository();
            //_accountRepository = new AccountRepository();
        }
        [BindProperty]
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
            Tutor = tutor;
            //ViewData["AccountId"] = new SelectList(_accountRepository.GetAllAccounts(), "Id", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            try
            {
                _tutorRepository.UpdateTutor(Tutor);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception();
            }

            return RedirectToPage("./TutorIndex");
        }


    }
}
