using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IFeedbackRepository
    {
        IEnumerable<Feedback> GetAllFeedbacks();
        Feedback GetFeedbackById(int feedbackId);
        void AddFeedback(Feedback feedback);
        void UpdateFeedback(Feedback feedback);
        void DeleteFeedback(int feedbackId);
    }

}
