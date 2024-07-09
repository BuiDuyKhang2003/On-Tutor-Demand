using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class TutorRegistrationDAO
    {
        private static AppDbContext db = new();

        public static List<TutorRegistration> GetAllTutorRegistrations()
        {
            return db.TutorRegistrations.ToList();
        }

        public async static Task<TutorRegistration> GetTutorRegistrationById(int registrationId)
        {
            return await db.TutorRegistrations.FindAsync(registrationId);
        }

        public async static void AddTutorRegistration(TutorRegistration tutorRegistration)
        {
            await db.TutorRegistrations.AddAsync(tutorRegistration);
            await db.SaveChangesAsync();
        }

        public async static void UpdateTutorRegistration(TutorRegistration tutorRegistration)
        {
            db = new();
            var existingRegistration = await db.TutorRegistrations
                                           .FirstOrDefaultAsync(tr => tr.Id == tutorRegistration.Id);

            if (existingRegistration != null)
            {
                existingRegistration.IsApproved = tutorRegistration.IsApproved;

                db.Entry(existingRegistration).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Tutor registration not found.");
            }
        }

        public async static void DeleteTutorRegistration(int registrationId)
        {
            var registration = db.TutorRegistrations.Find(registrationId);
            if (registration != null)
            {
                db.TutorRegistrations.Remove(registration);
                await db.SaveChangesAsync();
            }
        }
    }
}
