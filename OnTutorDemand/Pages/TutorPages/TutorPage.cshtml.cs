using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnTutorDemand.Pages.TutorPages
{
    public class TutorPageModel : PageModel
    {
        public TutorInfo Tutor { get; set; }

        public void OnGet()
        {
            // Dummy data for demonstration purposes
            Tutor = new TutorInfo
            {
                FullName = "Nguy?n V?n A",
                Gender = "Nam",
                BirthYear = 1985,
                Education = "Th?c S?",
                Experience = "10 n?m",
                Subjects = "To�n, L�, H�a",
                Districts = "Qu?n 1, Qu?n 3, Qu?n 5",
                Grades = "L?p 10, L?p 11, L?p 12",
                Description = "C� nhi?u kinh nghi?m gi?ng d?y c�c l?p c?p 3, ph??ng ph�p gi?ng d?y d? hi?u, chuy�n s�u c�c m�n To�n, L�, H�a."
            };
        }

        public class TutorInfo
        {
            public string FullName { get; set; }
            public string Gender { get; set; }
            public int BirthYear { get; set; }
            public string Education { get; set; }
            public string Experience { get; set; }
            public string Subjects { get; set; }
            public string Districts { get; set; }
            public string Grades { get; set; }
            public string Description { get; set; }
        }
    }
}
