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
    public class TutorRegistrationRepository : ITutorRegistrationRepository
    {
        public async Task<IEnumerable<TutorRegistration>> GetAllTutorRegistrations() => await TutorRegistrationDAO.GetAllTutorRegistrationsAsync();
        public async Task<TutorRegistration> GetTutorRegistrationById(int registrationId) => await TutorRegistrationDAO.GetTutorRegistrationByIdAsync(registrationId);
        public void AddTutorRegistration(TutorRegistration tutorRegistration) => TutorRegistrationDAO.AddTutorRegistrationAsync(tutorRegistration);
        public void UpdateTutorRegistration(TutorRegistration tutorRegistration) => TutorRegistrationDAO.UpdateTutorRegistrationAsync(tutorRegistration);
        public void DeleteTutorRegistration(int registrationId) => TutorRegistrationDAO.DeleteTutorRegistrationAsync(registrationId);
    }
}
