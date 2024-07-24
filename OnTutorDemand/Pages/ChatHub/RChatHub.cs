using BusinessObject;
using Microsoft.AspNetCore.SignalR;
using Repository;
using Repository.RepositoryInterface;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnTutorDemand.Pages.ChatHub
{
    public class RChatHub : Hub
    {
        private readonly IChatMessageRepository chatMessageRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IConversationRepository conversationRepository;

        public RChatHub(IChatMessageRepository chatMessageRepository, IAccountRepository accountRepository, IConversationRepository conversationRepository)
        {
            this.chatMessageRepository = chatMessageRepository;
            this.accountRepository = accountRepository;
            this.conversationRepository = conversationRepository;
        }

        public async Task SendMessage(int conversationId, int senderId, string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentException("Message cannot be empty", nameof(message));
                }

                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://worldtimeapi.org/api/timezone/Etc/UTC");
                var jsonDocument = JsonDocument.Parse(response);
                var datetimeString = jsonDocument.RootElement.GetProperty("datetime").GetString();
                var vietNamTime = DateTime.SpecifyKind(DateTime.Parse(datetimeString), DateTimeKind.Utc);

                var chatMessage = new ChatMessage
                {
                    ConversationId = conversationId,
                    SenderId = senderId,
                    Content = message,
                    SentDate = vietNamTime,
                    EditedDate = vietNamTime
                };

                Account sender = await accountRepository.GetAccountById(senderId);

                await chatMessageRepository.AddChatMessage(chatMessage);

                Conversation currentConversation = await conversationRepository.GetConversationById(conversationId);

                currentConversation.LastMessageDate = vietNamTime;

                conversationRepository.UpdateConversation(currentConversation);

                await Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", sender.FullName, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendMessage: {ex.Message}");
                throw;
            }
        }


        public async Task JoinConversation(int conversationId, int userId)
        {
            if (conversationId != 0)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
                await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");

                var conversation = await conversationRepository.GetConversationById(conversationId);
                if (conversation != null && (conversation.InitiatorId == userId || conversation.ReceiverId == userId))
                {
                    var otherUserId = conversation.ReceiverId;
                    var currentUser = await accountRepository.GetAccountById(userId);

                    await Clients.Group($"User_{otherUserId}").SendAsync("NewConversation", conversationId, currentUser.FullName, conversation.LastMessageDate);
                }
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");
            }

        }
    }
}
