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
        private static AppDbContext db = new();

        public static List<Tutor> GetAllTutors()
        {
            return db.Tutors.ToList();
        }

        public static Tutor GetTutorById(int tutorId)
        {
            return db.Tutors.Find(tutorId) ?? new Tutor();
        }

        public static void AddTutor(Tutor tutor)
        {
            db.Tutors.Add(tutor);
            db.SaveChanges();
        }

        public static void UpdateTutor(Tutor tutor)
        {
            db.Entry(tutor).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteTutor(int tutorId)
        {
            var tutor = db.Tutors.Find(tutorId);
            if (tutor != null)
            {
                db.Tutors.Remove(tutor);
                db.SaveChanges();
            }
        }
    }
}
