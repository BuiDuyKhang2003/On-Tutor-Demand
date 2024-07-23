using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                List<string> grades = existingRegistration.TeachingGrades.Split(",").Select(g => g.Trim().ToLower()).ToList();
                List<string> subjects = existingRegistration.TeachingSubjects.Split(",").Select(s => s.Trim().ToLower()).ToList();
                List<string> areas = existingRegistration.TeachingAreas.Split(",").Select(a => a.Trim().ToLower()).ToList();

                var gradeList = await db.Grades.Where(g => grades.Contains(g.GradeName.ToLower().Trim())).ToListAsync();
                var subjectList = await db.Subjects.Where(s => subjects.Contains(s.SubjectName.ToLower().Trim())).ToListAsync();
                var districtList = await db.Districts.Where(d => areas.Contains(d.DistrictName.ToLower().Trim())).ToListAsync();

                Tutor tutor = new Tutor
                {
                    AccountId = newUser.Id,
                    Education = existingRegistration.Education,
                    Experience = existingRegistration.Experience,
                    Description = existingRegistration.Description,
                    TutorAreas = districtList.Select(d => new TutorArea { DistrictId = d.Id }).ToList(),
                    TutorGrades = gradeList.Select(g => new TutorGrade { GradeId = g.Id }).ToList(),
                    TutorSubjects = subjectList.Select(s => new TutorSubject { SubjectId = s.Id }).ToList()
                };

                await TutorDAO.AddTutorAsync(tutor);
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
