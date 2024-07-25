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
    public class TutorRepository : ITutorRepository
    {
        public async Task AddTutor(Tutor tutor) => await TutorDAO.AddTutorAsync(tutor);

        public void DeleteTutor(int? tutorId) => TutorDAO.DeleteTutor(tutorId);

        public async Task<Tutor> GetTutorById(int? tutorId) => await TutorDAO.GetTutorById(tutorId);

        public IEnumerable<Tutor> GetAllTutors() => TutorDAO.GetAllTutors();

        public void UpdateTutor(Tutor tutor) => TutorDAO.UpdateTutor(tutor);

        public int GetTotalTutorsCount() => TutorDAO.GetTotalTutorsCount();

        public IQueryable<Tutor> GetTutorServicesByQuery() => TutorDAO.GetTutorServicesByQuery();

        public async Task<IEnumerable<District>> GetAllDistricts() => await TutorDAO.GetAllDistricts();

        public async Task<IEnumerable<Subject>> GetAllSubjects() => await TutorDAO.GetAllSubjects();

        public async Task<IEnumerable<Grade>> GetAllGrades() => await TutorDAO.GetAllGrades();
    }
}
