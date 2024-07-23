using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.RepositoryInterface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTutorDemand.Pages.ChatPages
{
    public class ChatPageModel : PageModel
    {
        private readonly IChatMessageRepository chatMessageRepository;
        private readonly IConversationRepository conversationRepository;
        private readonly IAccountRepository accountRepository;

        public ChatPageModel(IChatMessageRepository chatMessageRepository, IConversationRepository conversationRepository, IAccountRepository accountRepository)
        {
            this.chatMessageRepository = chatMessageRepository;
            this.conversationRepository = conversationRepository;
            this.accountRepository = accountRepository;
        }

        [BindProperty]
        public Conversation Conversation { get; set; }

        [BindProperty]
        public List<ChatMessage> ChatMessages { get; set; }

        [BindProperty]
        public List<Conversation> PreviousConversations { get; set; }

        [BindProperty]
        public int UserId { get; set; }
        
        public string FormatRelativeDate(DateTime dateTime)
        {
            var span = DateTime.Now - dateTime;

            if (span.TotalMinutes < 60)
                return $"{(int)span.TotalMinutes}m";
            if (span.TotalHours < 24)
                return $"{(int)span.TotalHours}h";
            if (span.TotalDays < 7)
                return $"{(int)span.TotalDays}d";
            return $"{(int)(span.TotalDays / 7)}w";
        }

        public async Task<IActionResult> OnGetAsync(int conversationId)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var currentAccount = await accountRepository.GetAccountByEmail(userEmail);
            if (String.IsNullOrEmpty(currentAccount.Email))
            {
                return RedirectToPage("/Authenticate/Login");
            }

            UserId = currentAccount.Id;
            PreviousConversations = (await conversationRepository.GetConversationsByUserId(UserId)).ToList();

            Conversation = await conversationRepository.GetConversationById(conversationId);
            if (Conversation == null)
            {
                return NotFound();
            }

            ChatMessages = (await chatMessageRepository.GetChatMessagesByConversationId(conversationId)).ToList();
            return Page();
        }
    }
}
