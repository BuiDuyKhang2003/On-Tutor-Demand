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

namespace OnTutorDemand.Pages.TutorPages
{
    public class TutorIndexModel : PageModel
    {
        private readonly ITutorRepository tutorRepository;

        public TutorIndexModel(ITutorRepository tutorRepository)
        {
            this.tutorRepository = tutorRepository;
        }

        public List<Tutor> Tutors { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }

        public void OnGet(int pageNumber = 1)
        {
            CurrentPage = pageNumber;
            if (tutorRepository.GetAllTutors(CurrentPage, PageSize) != null)
            {
                Tutors = tutorRepository.GetAllTutors(pageNumber, PageSize).ToList();
            }
            else
            {
                Tutors = new List<Tutor>();
            }
            int totalTutors = tutorRepository.GetTotalTutorsCount(); 
            TotalPages = (int)Math.Ceiling(totalTutors / (double)PageSize);
        }
    }
}
