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
        private static AppDbContext db;

        public async static Task<List<BookingSchedule>> GetBookingHistoryAsync(int accountId)
        {
            return await db.BookingSchedules
                .Include(bs => bs.Schedule)
                .Where(bs => bs.AccountId == accountId)
                .ToListAsync();
        }

        public async static Task<bool> BookScheduleAsync(int scheduleId, int accountId)
        {
            var schedule = await db.Schedules.FindAsync(scheduleId);
            if (schedule == null || db.BookingSchedules.Any(bs => bs.ScheduleId == scheduleId))
                return false;

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://worldtimeapi.org/api/timezone/Etc/UTC");
            var jsonDocument = JsonDocument.Parse(response);
            var datetimeString = jsonDocument.RootElement.GetProperty("datetime").GetString();
            var vietNamTime = DateTime.SpecifyKind(DateTime.Parse(datetimeString), DateTimeKind.Utc);

            var bookingSchedule = new BookingSchedule
            {
                ScheduleId = scheduleId,
                AccountId = accountId,
                BookingDate = vietNamTime,
                Status = "unpaid"
            };

            db.BookingSchedules.Add(bookingSchedule);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
