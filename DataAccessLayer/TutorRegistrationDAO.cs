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

        public async static Task<List<TutorRegistration>> GetAllTutorRegistrationsAsync()
        {
            db = new();
            return await db.TutorRegistrations.ToListAsync();
        }

        public async static Task<TutorRegistration> GetTutorRegistrationByIdAsync(int registrationId)
        {
            db = new();
            return await db.TutorRegistrations.FindAsync(registrationId);
        }

        public async static void AddTutorRegistrationAsync(TutorRegistration tutorRegistration)
        {
            db = new();
            await db.TutorRegistrations.AddAsync(tutorRegistration);
            await db.SaveChangesAsync();
        }

        public async static Task UpdateTutorRegistrationAsync(TutorRegistration tutorRegistration)
        {
            db = new();
            var existingRegistration = await db.TutorRegistrations
                                           .FirstOrDefaultAsync(tr => tr.Email.Trim().Equals(tutorRegistration.Email.Trim()));

            if (existingRegistration != null)
            {
                existingRegistration.Status = tutorRegistration.Status;
                await db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Tutor registration not found.");
            }
            var existAccount = await AccountDAO.GetAccountByEmailAsync(existingRegistration.Email);
            var existEmail = existAccount.Email;
            if (existingRegistration.Status.Equals("Approved") && existEmail == null) 
            {
                var user = new Account
                {
                    Email = existingRegistration.Email,
                    Password = existingRegistration.Password,
                    FullName = existingRegistration.FullName,
                    DateOfBirth = existingRegistration.DateOfBirth,
                    Phone = existingRegistration.Phone,
                    Gender = existingRegistration.Gender,
                    Address = existingRegistration.Address,
                    Role = "Tutor",
                    IsActive = true
                };

                Account newUser = await AccountDAO.AddAccountAsync(user);
                await db.SaveChangesAsync();

                Tutor tutor = new Tutor
                {
                    AccountId = newUser.Id,
                    Education = existingRegistration.Education,
                    Experience = existingRegistration.Experience,
                    Description = existingRegistration.Description
                };

                TutorDAO.AddTutor(tutor);
                await db.SaveChangesAsync();
            }           
        }

        public async static void DeleteTutorRegistrationAsync(int registrationId)
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
