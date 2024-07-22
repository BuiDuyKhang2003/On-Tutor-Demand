using System;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject;
using Repository.RepositoryInterface;

namespace OnTutorDemand.Pages.ModeratorPages
{
    public class ApproveRegistrationModel : PageModel
    {
        private readonly ITutorRegistrationRepository tutorRegistrationRepository;

        public ApproveRegistrationModel(ITutorRegistrationRepository tutorRegistrationRepository)
        {
            this.tutorRegistrationRepository = tutorRegistrationRepository;
        }

        [BindProperty]
        public TutorRegistration TutorRegistration { get; set; } = default!;
        
        [BindProperty]
        public string Email { get; set; } = default!;

        [BindProperty]
        public string Description { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorregistration = await tutorRegistrationRepository.GetTutorRegistrationById((int)id);
            if (tutorregistration == null)
            {
                return NotFound();
            }
            TutorRegistration = tutorregistration;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("TutorRegistration.Password");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await tutorRegistrationRepository.UpdateTutorRegistration(TutorRegistration);
            await SendEmailAsync(Email, "Tutor Registration " + TutorRegistration.Status, Description);

            return RedirectToPage("./TutorRegistrationPage");
        }
        
        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("styematic@gmail.com", "tcqcqeigvocszmas"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("styematic@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to send email", ex);
            }
        }
    }
}
