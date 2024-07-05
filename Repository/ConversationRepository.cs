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
    public class ConversationRepository : IConversationRepository
    {
        public void AddConversation(Conversation conversation) => ConversationDAO.AddConversation(conversation);

        public void DeleteConversation(int conversationId) => ConversationDAO.DeleteConversation(conversationId);

        public Conversation GetConversationById(int conversationId) => ConversationDAO.GetConversationById(conversationId);

        public IEnumerable<Conversation> GetAllConversations() => ConversationDAO.GetAllConversations();

        public void UpdateConversation(Conversation conversation) => ConversationDAO.UpdateConversation(conversation);
    }
}
