using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RentalServiceDAO
    {
        private static AppDbContext db = new();
        public static IQueryable<RentalService> GetRentalServicesByQuery()
        {
            return from s in db.RentalServices.Include(x=>x.Tutor).ThenInclude(x => x.Account)
                                              .Include(t => t.Tutor).ThenInclude(t => t.TutorSubjects).ThenInclude(ts => ts.Subject)
                                              .Include(x => x.Tutor).ThenInclude(d=>d.TutorAreas).ThenInclude(ta=>ta.District)
                                              .Include(x => x.Tutor).ThenInclude(tg=>tg.TutorGrades).ThenInclude(g=>g.Grade)
                   select s;
        }
        public static List<RentalService> GetAllRentalServices()
        {
            return db.RentalServices.Include(r=>r.Tutor).ThenInclude(x=>x.Account).ToList();
        }

        public async static Task<RentalService> GetRentalServiceById(int? rentalServiceId)
        {
            return db.RentalServices.Include(r => r.Tutor).ThenInclude(x => x.Account).FirstOrDefault(s => s.Id == rentalServiceId) ?? new RentalService();
        }

        public async static Task AddRentalService(RentalService rentalService)
        {
            await db.RentalServices.AddAsync(rentalService);
            await db.SaveChangesAsync();
        }

        public static async Task<RentalService> UpdateRentalService(RentalService rentalService)
        {
            db = new();
            var existingService = await db.RentalServices
                                           .FirstOrDefaultAsync(s => s.Id == rentalService.Id && s.TutorId == rentalService.TutorId);

            if (existingService != null)
            {
                existingService.TutorId = rentalService.TutorId;
                existingService.ServiceName = rentalService.ServiceName;
                existingService.Description = rentalService.Description;
                existingService.PricePerSession = rentalService.PricePerSession;
                db.Entry(existingService).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return rentalService;
            }
            else
            {
                throw new Exception("Service not found.");
            }
        }
        public static async Task DeleteRentalService(int? rentalServiceId)
        {
            db = new();
            var rentalService = db.RentalServices.Include(r => r.Tutor).ThenInclude(x => x.Account).FirstOrDefault(s=> s.Id == rentalServiceId);
            if (rentalService != null)
            {
                db.RentalServices.Remove(rentalService);
                await db.SaveChangesAsync();
            }
            
        }
    }
}
