using BusinessObject;
using DataAccessLayer;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        public Task AddChatMessage(ChatMessage chatMessage) => ChatMessageDAO.AddChatMessageAsync(chatMessage);

        public Task DeleteChatMessage(int chatMessageId) => ChatMessageDAO.DeleteChatMessageAsync(chatMessageId);

        public async Task<ChatMessage> GetChatMessageById(int chatMessageId) => await ChatMessageDAO.GetChatMessageByIdAsync(chatMessageId);

        public async Task<IEnumerable<ChatMessage>> GetAllChatMessages() => await ChatMessageDAO.GetAllChatMessagesAsync();

        public Task UpdateChatMessage(ChatMessage chatMessage) => ChatMessageDAO.UpdateChatMessageAsync(chatMessage);

        public async Task<IEnumerable<ChatMessage>> GetChatMessagesByConversationId(int conversationId) => await ChatMessageDAO.GetChatMessagesByConversationIdAsync(conversationId);
    }
}