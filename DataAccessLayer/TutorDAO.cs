using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class TutorDAO
    {
        private static AppDbContext db;

        public static List<Tutor> GetAllTutors()
        {
            db = new();
            return db.Tutors.Include(x=>x.Account).ToList();
        }
        public static IQueryable<Tutor> GetTutorServicesByQuery()
        {
            db = new();
            return from s in db.Tutors.Include(t => t.Account)
                            .Include(t => t.TutorGrades)
                            .ThenInclude(tg => tg.Grade)
                            .Include(t => t.TutorAreas)
                            .ThenInclude(ta => ta.District)
                            .Include(t=>t.TutorSubjects)
                            .ThenInclude(ts=>ts.Subject)
                   select s;
        }
        public async static Task<Tutor> GetTutorById(int? tutorId)
        {
            db = new();
            return await db.Tutors
                .Include(t => t.Account)
                .Include(t => t.TutorGrades)
                .Include(t => t.TutorAreas)
                .Include(t => t.TutorSubjects)
                .FirstOrDefaultAsync(t => t.Id == tutorId);
        }

        public async static Task<List<Grade>> GetAllGrades()
        {
            db = new();
            return await db.Grades.ToListAsync();
        }

        public async static Task<List<District>> GetAllDistricts()
        {
            db = new();
            return await db.Districts.ToListAsync();
        }

        public async static Task<List<Subject>> GetAllSubjects()
        {
            db = new();
            return await db.Subjects.ToListAsync();
        }

        public static async Task AddTutorAsync(Tutor tutor)
        {
            db = new();
            {
                db.Tutors.Add(tutor);
                await db.SaveChangesAsync();
            }
        }

        public static void UpdateTutor(Tutor tutor)
        {
            db = new();
            db.Entry(tutor).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteTutor(int? tutorId)
        {
            db = new();
            var tutor = db.Tutors.Find(tutorId);
            if (tutor != null)
            {
                db.Tutors.Remove(tutor);
                db.SaveChanges();
            }
        }

        public static int GetTotalTutorsCount()
        {
            db = new();
            return db.Tutors.Count();
        }
    }
}
