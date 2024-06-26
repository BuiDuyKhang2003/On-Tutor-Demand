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

        public static List<RentalService> GetAllRentalServices()
        {
            return db.RentalServices.ToList();
        }

        public static RentalService GetRentalServiceById(int? rentalServiceId)
        {
            return db.RentalServices.Find(rentalServiceId) ?? new RentalService();
        }

        public static void AddRentalService(RentalService rentalService)
        {
            db.RentalServices.Add(rentalService);
            db.SaveChanges();
        }

        public static void UpdateRentalService(RentalService rentalService)
        {
            db = new();
            db.Entry(rentalService).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteRentalService(int? rentalServiceId)
        {
            var rentalService = db.RentalServices.Find(rentalServiceId);
            if (rentalService != null)
            {
                db.RentalServices.Remove(rentalService);
                db.SaveChanges();
            }
        }
    }
}
