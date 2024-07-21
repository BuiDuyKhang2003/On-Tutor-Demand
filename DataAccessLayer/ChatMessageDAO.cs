using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ChatMessageDAO
    {
        private static AppDbContext db;

        public async static Task<List<ChatMessage>> GetAllChatMessagesAsync()
        {
            db = new();
            return await db.ChatMessages.ToListAsync();
        }

        public async static Task<List<ChatMessage>> GetChatMessagesByConversationIdAsync(int conversationId)
        {
            db = new();
            return await db.ChatMessages.Include(c => c.Sender).Where(c => c.ConversationId == conversationId).ToListAsync();
        }

        public async static Task<ChatMessage> GetChatMessageByIdAsync(int chatMessageId)
        {
            db = new();
            return await db.ChatMessages.FindAsync(chatMessageId);
        }

        public async static Task AddChatMessageAsync(ChatMessage chatMessage)
        {
            db = new();
            await db.ChatMessages.AddAsync(chatMessage);
            await db.SaveChangesAsync();
        }

        public async static Task UpdateChatMessageAsync(ChatMessage chatMessage)
        {
            db = new();
            db.Entry(chatMessage).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async static Task DeleteChatMessageAsync(int chatMessageId)
        {
            db = new();
            var chatMessage = await db.ChatMessages.FindAsync(chatMessageId);
            if (chatMessage != null)
            {
                db.ChatMessages.Remove(chatMessage);
                await db.SaveChangesAsync();
            }
        }
    }

}
