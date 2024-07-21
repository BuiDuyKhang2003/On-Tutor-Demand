using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IChatMessageRepository
    {
        Task<IEnumerable<ChatMessage>> GetAllChatMessages();
        Task<IEnumerable<ChatMessage>> GetChatMessagesByConversationId(int conversationId);
        Task<ChatMessage> GetChatMessageById(int chatMessageId);
        Task AddChatMessage(ChatMessage chatMessage);
        Task UpdateChatMessage(ChatMessage chatMessage);
        Task DeleteChatMessage(int chatMessageId);
    }
}
