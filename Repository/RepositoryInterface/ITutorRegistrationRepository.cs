using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface ITutorRegistrationRepository
    {
        Task<IEnumerable<TutorRegistration>> GetAllTutorRegistrations();
        Task<TutorRegistration> GetTutorRegistrationById(int registrationId);
        void AddTutorRegistration(TutorRegistration tutorRegistration);
        void UpdateTutorRegistration(TutorRegistration tutorRegistration);
        void DeleteTutorRegistration(int registrationId);
    }
}
