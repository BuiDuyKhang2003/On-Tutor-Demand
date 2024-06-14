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
        IEnumerable<Conversation> GetAllConversations();
        Conversation GetConversationById(int conversationId);
        void AddConversation(Conversation conversation);
        void UpdateConversation(Conversation conversation);
        void DeleteConversation(int conversationId);
    }
}
