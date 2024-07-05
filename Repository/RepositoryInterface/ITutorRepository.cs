using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface ITutorRepository
    {
        IEnumerable<Tutor> GetAllTutors(int pageNumber, int pageSize);
        Tutor GetTutorById(int tutorId);
        void AddTutor(Tutor tutor);
        void UpdateTutor(Tutor tutor);
        void DeleteTutor(int tutorId);
        int GetTotalTutorsCount();
    }
}
