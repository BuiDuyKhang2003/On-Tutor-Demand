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
        public IEnumerable<TutorRegistration> GetAllTutorRegistrations() => TutorRegistrationDAO.GetAllTutorRegistrations();
        public async Task<TutorRegistration> GetTutorRegistrationById(int registrationId) => await TutorRegistrationDAO.GetTutorRegistrationById(registrationId);
        public void AddTutorRegistration(TutorRegistration tutorRegistration) => TutorRegistrationDAO.AddTutorRegistration(tutorRegistration);
        public void UpdateTutorRegistration(TutorRegistration tutorRegistration) => TutorRegistrationDAO.UpdateTutorRegistration(tutorRegistration);
        public void DeleteTutorRegistration(int registrationId) => TutorRegistrationDAO.DeleteTutorRegistration(registrationId);
    }
}
