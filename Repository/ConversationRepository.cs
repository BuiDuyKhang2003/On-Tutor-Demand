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
        public async Task<Conversation> AddConversation(Conversation conversation) => await ConversationDAO.AddConversationAsync(conversation);

        public void DeleteConversation(int conversationId) => ConversationDAO.DeleteConversationAsync(conversationId);

        public async Task<Conversation> GetConversationById(int conversationId) => await ConversationDAO.GetConversationByIdAsync(conversationId);

        public async Task<Conversation> GetMostRecentConversationByAccountId(int accountId) => await ConversationDAO.GetMostRecentConversationByAccountIdAsync(accountId);

        public async Task<IEnumerable<Conversation>> GetAllConversations() => await ConversationDAO.GetAllConversationsAsync();

        public void UpdateConversation(Conversation conversation) => ConversationDAO.UpdateConversationAsync(conversation);

        public async Task<Conversation> GetConversationByInitiatorIdAndReceiverIdAsync(int initiatorId, int receiverId) => await ConversationDAO.GetConversationByInitiatorIdAndReceiverIdAsync(initiatorId, receiverId);

        public async Task<IEnumerable<Conversation>> GetConversationsByUserId(int initiatorId) => await ConversationDAO.GetConversationsByUserIdAsync(initiatorId);
    }
}
