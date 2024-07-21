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

        public static List<Tutor> GetAllTutors(int pageNumber, int pageSize)
        {
            db = new();
            return db.Tutors.Include(t => t.Account)
                            .Include(t => t.TutorGrades)
                            .ThenInclude(tg => tg.Grade)
                            .Include(t => t.TutorAreas)
                            .ThenInclude(ta => ta.District)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
        }

        public static Tutor GetTutorById(int? tutorId)
        {
            db = new();
            return db.Tutors.Find(tutorId) ?? new Tutor();
        }

        public static void AddTutor(Tutor tutor)
        {
            db = new();
            db.Tutors.Add(tutor);
            db.SaveChanges();
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
