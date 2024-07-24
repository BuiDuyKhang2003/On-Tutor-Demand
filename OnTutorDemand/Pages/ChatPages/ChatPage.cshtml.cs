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

        [BindProperty(SupportsGet = true)]
        public bool IsNew { get; set; } = false;

        [BindProperty]
        public string CurrentUserName { get; set; }

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

        public async Task<IActionResult> OnGetConversationAsync(int conversationId, bool isNew = false)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var currentAccount = await accountRepository.GetAccountByEmail(userEmail);
            if (String.IsNullOrEmpty(currentAccount.Email))
            {
                return RedirectToPage("/Authenticate/Login");
            }

            CurrentUserName = currentAccount.FullName;

            UserId = currentAccount.Id;
            PreviousConversations = (await conversationRepository.GetConversationsByUserId(UserId)).ToList();

            Conversation = await conversationRepository.GetConversationById(conversationId);
            if (Conversation == null)
            {
                return Page();
            }

            ChatMessages = (await chatMessageRepository.GetChatMessagesByConversationId(conversationId)).ToList();
            IsNew = isNew;
            return Page();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var currentAccount = await accountRepository.GetAccountByEmail(userEmail);
            if (String.IsNullOrEmpty(currentAccount.Email))
            {
                return RedirectToPage("/Authenticate/Login");
            }

            UserId = currentAccount.Id;
            var mostRecentConversation = await conversationRepository.GetMostRecentConversationByAccountId(UserId);

            if (mostRecentConversation != null)
            {
                return RedirectToPage(new { handler = "Conversation", conversationId = mostRecentConversation.Id });
            }
            else
            {
                return Page();
            }
        }
    }
}
