using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryInterface;
using System.Text.Json;

namespace OnTutorDemand.Pages.RentalServicePage;

public class TutorDetail : PageModel
{
    private readonly IConversationRepository conversationRepository;
    private readonly IAccountRepository accountRepository;

    public TutorDetail(IConversationRepository conversationRepository, IAccountRepository accountRepository)
    {
        this.conversationRepository = conversationRepository;
        this.accountRepository = accountRepository;
    }
    public IActionResult OnGet()
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole == null || !userRole.Equals("User"))
        {
            return RedirectToPage("/Authenticate/Login");
        }

        return Page();
    }

    public async Task<IActionResult> OnGetStartChatAsync(int tutorId, string tutorName)
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");
        var currentAccount = await accountRepository.GetAccountByEmail(userEmail);
        if (currentAccount.Email == null) { 
            return NotFound();
        }

        Conversation existConversation = await conversationRepository.GetConversationByInitiatorIdAndReceiverIdAsync(currentAccount.Id, tutorId);
        if (existConversation != null)
        {
            return RedirectToPage("/ChatPages/ChatPage", new { conversationid = existConversation.Id });
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
            return RedirectToPage("/ChatPages/ChatPage", new { handler = "Conversation", conversationId = addedConversation.Id });
        }
    }
}