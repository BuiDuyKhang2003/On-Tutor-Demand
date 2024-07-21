using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject;
using Repository.RepositoryInterface;
using OnTutorDemand.Pages.PaginatedListFeature;
using Microsoft.EntityFrameworkCore;

namespace OnTutorDemand.Pages.TutorPages
{
    public class TutorIndexModel : PageModel
    {
        private readonly ITutorRepository tutorRepository;
        private IConfiguration _configuration;
        public TutorIndexModel(ITutorRepository tutorRepository, IConfiguration configuration)
        {
            this.tutorRepository = tutorRepository;
            _configuration = configuration;
        }

        public string eduSort { get; set; }
        public string desSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }
        public PaginatedList<Tutor> Tutors { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex, string education, string subject, string grade, string district)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || (!userRole.Equals("Tutor") && !userRole.Equals("User")))
            {
                RedirectToPage("/Authenticate/LoginRegisterPage");
            }

            CurrentSort = sortOrder;
            desSort = String.IsNullOrEmpty(sortOrder) ? "Description_desc" : "";
            eduSort = String.IsNullOrEmpty(sortOrder) ? "Education_desc" : "";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            IQueryable<Tutor> tutorIQ = tutorRepository.GetTutorServicesByQuery();

            if (!String.IsNullOrEmpty(searchString))
            {
                tutorIQ = tutorIQ.Where(s => s.Education.ToUpper().Contains(searchString)
                    || s.TutorAreas.Any(ta => ta.District.DistrictName.ToUpper().Contains(searchString))
                    || s.TutorGrades.Any(ta => ta.Grade.GradeName.ToUpper().Contains(searchString))
                    || s.TutorSubjects.Any(ta => ta.Subject.SubjectName.ToUpper().Contains(searchString))
                );
            }

            if (!String.IsNullOrEmpty(education))
            {
                tutorIQ = tutorIQ.Where(t => t.Education.ToUpper().Contains(education.ToUpper()));
            }

            if (!String.IsNullOrEmpty(subject))
            {
                tutorIQ = tutorIQ.Where(t => t.TutorSubjects.Any(ts => ts.Subject.SubjectName.ToUpper().Contains(subject.ToUpper())));
            }

            if (!String.IsNullOrEmpty(grade))
            {
                tutorIQ = tutorIQ.Where(t => t.TutorGrades.Any(tg => tg.Grade.GradeName.ToUpper().Contains(grade.ToUpper())));
            }

            if (!String.IsNullOrEmpty(district))
            {
                tutorIQ = tutorIQ.Where(t => t.TutorAreas.Any(ta => ta.District.DistrictName.ToUpper().Contains(district.ToUpper())));
            }

            switch (sortOrder)
            {
                case "Description_desc":
                    tutorIQ = tutorIQ.OrderByDescending(s => s.Description);
                    break;
                case "Education_desc":
                    tutorIQ = tutorIQ.OrderByDescending(s => s.Education);
                    break;
                default:
                    tutorIQ = tutorIQ.OrderBy(s => s.Description);
                    break;
            }

            var pageSize = _configuration.GetValue("PageSize", 6);
            Tutors = await PaginatedList<Tutor>.CreateAsync(
                tutorIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
