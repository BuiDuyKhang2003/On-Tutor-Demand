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
        private static AppDbContext db;

        public async static Task<List<Conversation>> GetAllConversationsAsync()
        {
            db = new();
            return await db.Conversations.ToListAsync();
        }

        public async static Task<List<Conversation>> GetConversationsByUserIdAsync(int accountId)
        {
            db = new();
            return await db.Conversations.Where(c => c.InitiatorId == accountId || c.ReceiverId == accountId).ToListAsync();
        }

        public async static Task<Conversation> GetConversationByIdAsync(int conversationId)
        {
            db = new();
            return await db.Conversations
                   .Include(c => c.Initiator)
                   .Include(c => c.Receiver)
                   .FirstOrDefaultAsync(c => c.Id == conversationId);
        }
        public async static Task<Conversation> GetConversationByInitiatorIdAndReceiverIdAsync(int studentId, int tutorId)
        {
            db = new();
            return await db.Conversations.FirstOrDefaultAsync(c => c.InitiatorId == studentId && c.ReceiverId == tutorId);
        }

        public async static Task<Conversation> GetMostRecentConversationByAccountIdAsync(int accountId)
        {
            db = new();
            return await db.Conversations
                .Where(c => c.InitiatorId == accountId || c.ReceiverId == accountId)
                .OrderByDescending(c => c.LastMessageDate)
                .FirstOrDefaultAsync();
        }


        public async static Task<Conversation> AddConversationAsync(Conversation conversation)
        {
            db = new();
            await db.Conversations.AddAsync(conversation);
            await db.SaveChangesAsync();
            return conversation;
        }

        public async static void UpdateConversationAsync(Conversation conversation)
        {
            db = new();
            db.Entry(conversation).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async static void DeleteConversationAsync(int conversationId)
        {
            db = new();
            var conversation = await db.Conversations.FindAsync(conversationId);
            if (conversation != null)
            {
                db.Conversations.Remove(conversation);
                await db.SaveChangesAsync();
            }
        }
    }

}
