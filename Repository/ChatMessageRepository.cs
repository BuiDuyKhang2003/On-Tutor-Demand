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
        public void AddChatMessage(ChatMessage chatMessage) => ChatMessageDAO.AddChatMessage(chatMessage);

        public void DeleteChatMessage(int chatMessageId) => ChatMessageDAO.DeleteChatMessage(chatMessageId);

        public ChatMessage GetChatMessageById(int chatMessageId) => ChatMessageDAO.GetChatMessageById(chatMessageId);

        public IEnumerable<ChatMessage> GetAllChatMessages() => ChatMessageDAO.GetAllChatMessages();

        public void UpdateChatMessage(ChatMessage chatMessage) => ChatMessageDAO.UpdateChatMessage(chatMessage);
    }
}