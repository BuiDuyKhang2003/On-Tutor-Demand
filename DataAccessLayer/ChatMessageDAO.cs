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
        private static AppDbContext db = new();

        public static List<ChatMessage> GetAllChatMessages()
        {
            return db.ChatMessages.ToList();
        }

        public static ChatMessage GetChatMessageById(int chatMessageId)
        {
            return db.ChatMessages.Find(chatMessageId) ?? new ChatMessage();
        }

        public static void AddChatMessage(ChatMessage chatMessage)
        {
            db.ChatMessages.Add(chatMessage);
            db.SaveChanges();
        }

        public static void UpdateChatMessage(ChatMessage chatMessage)
        {
            db.Entry(chatMessage).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteChatMessage(int chatMessageId)
        {
            var chatMessage = db.ChatMessages.Find(chatMessageId);
            if (chatMessage != null)
            {
                db.ChatMessages.Remove(chatMessage);
                db.SaveChanges();
            }
        }
    }

}
