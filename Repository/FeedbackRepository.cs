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
    public class FeedbackRepository : IFeedbackRepository
    {
        public void AddFeedback(Feedback feedback) => FeedbackDAO.AddFeedback(feedback);

        public void DeleteFeedback(int feedbackId) => FeedbackDAO.DeleteFeedback(feedbackId);

        public Feedback GetFeedbackById(int feedbackId) => FeedbackDAO.GetFeedbackById(feedbackId);

        public IEnumerable<Feedback> GetAllFeedbacks() => FeedbackDAO.GetAllFeedbacks();

        public void UpdateFeedback(Feedback feedback) => FeedbackDAO.UpdateFeedback(feedback);
    }
}
