using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ScheduleDAO
    {
        private static AppDbContext db = new();
        public static IQueryable<Schedule> GetScheduleServicesByQuery()
        {
            return from s in db.Schedules
                   select s;
        }
        public static List<Schedule> GetAllSchedules()
        {
            return db.Schedules.ToList();
        }

        public async static Task<Schedule> GetScheduleById(int? scheduleId)
        {
            return await db.Schedules.FindAsync(scheduleId);
        }

        public async static Task<Schedule> AddScheduleAsync(Schedule schedule)
        {
            db = new();
            await db.Schedules.AddAsync(schedule);
            await db.SaveChangesAsync();
            return schedule;
        }

        public async static Task<Schedule> UpdateSchedule(Schedule schedule)
        {
            db = new();
            var existingSchedule = await db.Schedules
                                           .FirstOrDefaultAsync(s => s.Id == schedule.Id);

            if (existingSchedule != null)
            {
                existingSchedule.DayOfWeek = schedule.DayOfWeek;
                existingSchedule.StartTime = schedule.StartTime;
                existingSchedule.EndTime = schedule.EndTime;

                db.Entry(existingSchedule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return schedule;
            }
            else
            {
                throw new Exception("Schedule not found.");
            }
        }

        public async static Task<Schedule> DeleteSchedule(int? scheduleId)
        {
            var schedule = await db.Schedules.FindAsync(scheduleId);
            if (schedule != null)
            {
                db.Schedules.Remove(schedule);
                await db.SaveChangesAsync();
            }
            return schedule;
        }
    }
}
