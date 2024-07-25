using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookingScheduleDAO
    {
        private readonly AppDbContext db;

        public BookingScheduleDAO(AppDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<List<BookingSchedule>> GetBookingHistoryAsync(int accountId)
        {
            return await db.BookingSchedules
                .Include(bs => bs.Schedule)
                .ThenInclude(s => s.RentalService)
                .ThenInclude(rs => rs.Tutor)
                .ThenInclude(t => t.Account) // Include Account for Tutor
                .Where(bs => bs.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<bool> BookScheduleAsync(int scheduleId, int accountId)
        {
            var schedule = await db.Schedules.FindAsync(scheduleId);
            if (schedule == null || db.BookingSchedules.Any(bs => bs.ScheduleId == scheduleId))
                return false;

            var bookingSchedule = new BookingSchedule
            {
                ScheduleId = scheduleId,
                AccountId = accountId,
                BookingDate = DateTime.UtcNow,
                Status = "unpaid"
            };

            db.BookingSchedules.Add(bookingSchedule);
            await db.SaveChangesAsync();

            return true;
        }
    }
}