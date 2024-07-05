using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ConversationDAO
    {
        private static AppDbContext db = new();

        public static List<Conversation> GetAllConversations()
        {
            return db.Conversations.ToList();
        }

        public static Conversation GetConversationById(int conversationId)
        {
            return db.Conversations.Find(conversationId) ?? new Conversation();
        }

        public static void AddConversation(Conversation conversation)
        {
            db.Conversations.Add(conversation);
            db.SaveChanges();
        }

        public static void UpdateConversation(Conversation conversation)
        {
            db.Entry(conversation).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteConversation(int conversationId)
        {
            var conversation = db.Conversations.Find(conversationId);
            if (conversation != null)
            {
                db.Conversations.Remove(conversation);
                db.SaveChanges();
            }
        }
    }

}
