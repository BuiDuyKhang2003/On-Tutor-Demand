using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FeedbackDAO
    {
        private static AppDbContext db = new();

        public static List<Feedback> GetAllFeedbacks()
        {
            return db.Feedbacks.ToList();
        }

        public static Feedback GetFeedbackById(int feedbackId)
        {
            return db.Feedbacks.Find(feedbackId) ?? new Feedback();
        }

        public static void AddFeedback(Feedback feedback)
        {
            db.Feedbacks.Add(feedback);
            db.SaveChanges();
        }

        public static void UpdateFeedback(Feedback feedback)
        {
            db.Entry(feedback).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteFeedback(int feedbackId)
        {
            var feedback = db.Feedbacks.Find(feedbackId);
            if (feedback != null)
            {
                db.Feedbacks.Remove(feedback);
                db.SaveChanges();
            }
        }
    }

}
