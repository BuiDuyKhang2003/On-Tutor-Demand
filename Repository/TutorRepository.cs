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
        public void AddTutor(Tutor tutor) => TutorDAO.AddTutor(tutor);

        public void DeleteTutor(int tutorId) => TutorDAO.DeleteTutor(tutorId);

        public Tutor GetTutorById(int tutorId) => TutorDAO.GetTutorById(tutorId);

        public IEnumerable<Tutor> GetAllTutors(int pageNumber, int pageSize) => TutorDAO.GetAllTutors(pageNumber, pageSize);

        public void UpdateTutor(Tutor tutor) => TutorDAO.UpdateTutor(tutor);

        public int GetTotalTutorsCount() => TutorDAO.GetTotalTutorsCount();
    }
}
