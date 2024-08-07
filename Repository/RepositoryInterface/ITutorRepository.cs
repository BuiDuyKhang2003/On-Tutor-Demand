﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface ITutorRepository
    {
        IQueryable<Tutor> GetTutorServicesByQuery();
        IEnumerable<Tutor> GetAllTutors();
        Task<Tutor> GetTutorById(int? tutorId);
        Task<IEnumerable<District>> GetAllDistricts();
        Task<IEnumerable<Subject>> GetAllSubjects();
        Task<IEnumerable<Grade>> GetAllGrades();
        Task AddTutor(Tutor tutor);
        void UpdateTutor(Tutor tutor);
        void DeleteTutor(int? tutorId);
        int GetTotalTutorsCount();
    }
}
