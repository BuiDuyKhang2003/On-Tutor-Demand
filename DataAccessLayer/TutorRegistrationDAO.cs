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
        private static AppDbContext db;

        public static List<TutorRegistration> GetAllTutorRegistrations()
        {
            db = new();
            return db.TutorRegistrations.ToList();
        }

        public async static Task<TutorRegistration> GetTutorRegistrationById(int registrationId)
        {
            db = new();
            return await db.TutorRegistrations.FindAsync(registrationId);
        }

        public async static void AddTutorRegistration(TutorRegistration tutorRegistration)
        {
            db = new();
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
                await db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Tutor registration not found.");
            }
            var user = new Account
            {
                Email = tutorRegistration.Email,
                Password = tutorRegistration.Password,
                FullName = tutorRegistration.FullName,
                DateOfBirth = tutorRegistration.DateOfBirth,
                Phone = tutorRegistration.Phone,
                Gender = tutorRegistration.Gender,
                Address = tutorRegistration.Address,
                Role = "Tutor",
                IsActive = true
            };

            AccountDAO.AddAccount(user);

            Tutor tutor = new Tutor { 
                AccountId = user.Id,
                Education = tutorRegistration.Education,
                Experience = tutorRegistration.Experience,
                Description = tutorRegistration.Description
            };

            TutorDAO.AddTutor(tutor);
            await db.SaveChangesAsync();
        }

        public async static void DeleteTutorRegistration(int registrationId)
        {
            db = new();
            var registration = db.TutorRegistrations.Find(registrationId);
            if (registration != null)
            {
                db.TutorRegistrations.Remove(registration);
                await db.SaveChangesAsync();
            }
        }
    }
}
