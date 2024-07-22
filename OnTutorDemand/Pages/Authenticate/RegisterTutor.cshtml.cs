using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnTutorDemand.Dto;
using Repository.RepositoryInterface;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnTutorDemand.Pages.Authenticate
{
    public class RegisterTutorModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITutorRegistrationRepository registrationRepository;
        private readonly ITutorRepository tutorRepository;

        public RegisterTutorModel(IAccountRepository accountRepository, ITutorRegistrationRepository registrationRepository, ITutorRepository tutorRepository)
        {
            this.accountRepository = accountRepository;
            this.registrationRepository = registrationRepository;
            this.tutorRepository = tutorRepository;
        }

        [BindProperty]
        public RegisterInputModel RegisterModel { get; set; }

        public List<Grade> AvailableGrades { get; set; }
        public List<Subject> AvailableSubjects { get; set; }
        public List<District> AvailableAreas { get; set; }

        public async Task<IActionResult> OnGet()
        {
            AvailableGrades = (await tutorRepository.GetAllGrades()).ToList();
            AvailableSubjects = (await tutorRepository.GetAllSubjects()).ToList();
            AvailableAreas = (await tutorRepository.GetAllDistricts()).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostRegister()
        {
            ModelState.Remove("EmailLogin");
            ModelState.Remove("PasswordLogin");

            if (RegisterModel.Role != "tutor")
            {
                ModelState.AddModelError(string.Empty, "Invalid role for this page.");
                return Page();
            }

            AvailableGrades = (await tutorRepository.GetAllGrades()).ToList();
            AvailableSubjects = (await tutorRepository.GetAllSubjects()).ToList();
            AvailableAreas = (await tutorRepository.GetAllDistricts()).ToList();

            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://worldtimeapi.org/api/timezone/Etc/UTC");
                var jsonDocument = JsonDocument.Parse(response);
                var datetimeString = jsonDocument.RootElement.GetProperty("datetime").GetString();
                var vietNamTime = DateTime.SpecifyKind(DateTime.Parse(datetimeString), DateTimeKind.Utc);

                var tutorRegistration = new TutorRegistration
                {
                    FullName = RegisterModel.Name,
                    Gender = RegisterModel.Gender,
                    Email = RegisterModel.Email,
                    Password = RegisterModel.Password,
                    Address = RegisterModel.Address,
                    DateOfBirth = RegisterModel.Birthdate,
                    Phone = RegisterModel.Phone,
                    Experience = RegisterModel.Experience,
                    Education = RegisterModel.Education,
                    ApplicationDate = vietNamTime,
                    Description = RegisterModel.Description,
                    TeachingGrades = RegisterModel.SelectedGrades != null && AvailableGrades != null
                ? string.Join(",", RegisterModel.SelectedGrades.Select(id => AvailableGrades.FirstOrDefault(g => g.Id == id)?.GradeName ?? ""))
                : "",
                    TeachingSubjects = RegisterModel.SelectedSubjects != null && AvailableSubjects != null
                ? string.Join(",", RegisterModel.SelectedSubjects.Select(id => AvailableSubjects.FirstOrDefault(s => s.Id == id)?.SubjectName ?? ""))
                : "",
                    TeachingAreas = RegisterModel.SelectedAreas != null && AvailableAreas != null
                ? string.Join(",", RegisterModel.SelectedAreas.Select(id => AvailableAreas.FirstOrDefault(a => a.Id == id)?.DistrictName ?? ""))
                : "",
                    Status = "Waiting"
                };

                registrationRepository.AddTutorRegistration(tutorRegistration);
                return RedirectToPage("/Authenticate/Login");
            }

            return Page();
        }
    }
}
