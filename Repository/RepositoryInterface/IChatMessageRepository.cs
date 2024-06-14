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
        IEnumerable<ChatMessage> GetAllChatMessages();
        ChatMessage GetChatMessageById(int chatMessageId);
        void AddChatMessage(ChatMessage chatMessage);
        void UpdateChatMessage(ChatMessage chatMessage);
        void DeleteChatMessage(int chatMessageId);
    }
}
