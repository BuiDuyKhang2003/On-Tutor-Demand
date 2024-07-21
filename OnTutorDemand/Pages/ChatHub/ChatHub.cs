using BusinessObject;
using Microsoft.AspNetCore.SignalR;
using Repository.RepositoryInterface;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnTutorDemand.Pages.ChatHub
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageRepository chatMessageRepository;
        private readonly IAccountRepository accountRepository;

        public ChatHub(IChatMessageRepository chatMessageRepository, IAccountRepository accountRepository)
        {
            this.chatMessageRepository = chatMessageRepository;
            this.accountRepository = accountRepository;
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

                await Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", sender.FullName, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendMessage: {ex.Message}");
                throw;
            }
        }


        public async Task JoinConversation(int conversationId)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in JoinConversation: {ex.Message}");
                throw;
            }
        }
    }
}
