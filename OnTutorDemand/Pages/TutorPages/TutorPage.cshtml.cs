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
                FullName = "Nguyễn Văn A",
                Gender = "Nam",
                BirthYear = 1985,
                Education = "Thạc Sĩ",
                Experience = "10 năm",
                Subjects = "Toán, Lý, Hóa",
                Districts = "Quận 1, Quận 3, Quận 5",
                Grades = "Lớp 10, Lớp 11, Lớp 12",
                Description = "Có nhiều kinh nghiệm giảng dạy các lớp cấp 3, phương pháp giảng dạy dễ hiểu, chuyên sâu các môn Toán, Lý, Hóa."

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
