using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.RepositoryInterface;
using System.Text.Json;

namespace OnTutorDemand.Pages.TutorPages
{
    public class TutorPageModel : PageModel
    {
        private readonly ITutorRepository tutorRepository;
        private readonly IConversationRepository conversationRepository;
        private readonly IAccountRepository accountRepository;

        public TutorPageModel(ITutorRepository tutorRepository, IAccountRepository accountRepository, IConversationRepository conversationRepository)
        {
            this.tutorRepository = tutorRepository;
            this.conversationRepository = conversationRepository;
            this.accountRepository = accountRepository;
        }

        [BindProperty]
        public Tutor TutorInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Tutor existedTutor = await tutorRepository.GetTutorById(id);
            if (existedTutor == null)
            {
                return RedirectToPage("/TutorPages/TutorIndex");
            }
            TutorInfo = existedTutor;
            return Page();
        }

        public async Task<IActionResult> OnGetStartChatAsync(int tutorId, string tutorName)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var currentAccount = await accountRepository.GetAccountByEmail(userEmail);
            if (String.IsNullOrEmpty(currentAccount.Email))
            {
                return RedirectToPage("/Authenticate/Login"); ;
            }

            Conversation existConversation = await conversationRepository.GetConversationByInitiatorIdAndReceiverIdAsync(currentAccount.Id, tutorId);
            if (existConversation != null)
            {
                return RedirectToPage("/ChatPages/ChatPage", new { handler = "Conversation", conversationid = existConversation.Id });
            }
            else
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://worldtimeapi.org/api/timezone/Etc/UTC");
                var jsonDocument = JsonDocument.Parse(response);
                var datetimeString = jsonDocument.RootElement.GetProperty("datetime").GetString();
                var vietNamTime = DateTime.SpecifyKind(DateTime.Parse(datetimeString), DateTimeKind.Utc);

                var newConversation = new Conversation
                {
                    InitiatorId = currentAccount.Id,
                    ReceiverId = tutorId,
                    Subject = tutorName,
                    CreatedDate = vietNamTime,
                    LastMessageDate = vietNamTime
                };

                Conversation addedConversation = await conversationRepository.AddConversation(newConversation);

                return RedirectToPage("/ChatPages/ChatPage", new { handler = "Conversation", conversationId = addedConversation.Id, isNew = true });
            }
        }
    }
}
