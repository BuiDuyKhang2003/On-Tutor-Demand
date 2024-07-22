using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IConversationRepository
    {
        Task<IEnumerable<Conversation>> GetAllConversations();
        Task<IEnumerable<Conversation>> GetConversationsByUserId(int initiatorId);
        Task<Conversation> GetConversationById(int conversationId);
        Task<Conversation> GetMostRecentConversationByAccountId(int accountId);
        Task<Conversation> GetConversationByInitiatorIdAndReceiverIdAsync(int initiatorId, int receiverId);
        Task<Conversation> AddConversation(Conversation conversation);
        void UpdateConversation(Conversation conversation);
        void DeleteConversation(int conversationId);
    }
}
