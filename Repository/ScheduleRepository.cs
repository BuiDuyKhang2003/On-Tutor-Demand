using BusinessObject;
using DataAccessLayer;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        public IEnumerable<Schedule> GetAllSchedules() => ScheduleDAO.GetAllSchedules();

        public async Task<Schedule> GetScheduleById(int? scheduleId) => await ScheduleDAO.GetScheduleById(scheduleId);

        public async Task<Schedule> AddScheduleAsync(Schedule schedule) => await ScheduleDAO.AddScheduleAsync(schedule);

        public async Task<Schedule> UpdateSchedule(Schedule schedule) => await ScheduleDAO.UpdateSchedule(schedule);

        public async Task<Schedule> DeleteSchedule(int? scheduleId) => await ScheduleDAO.DeleteSchedule(scheduleId);

        public IQueryable<Schedule> GetScheduleServicesByQuery() => ScheduleDAO.GetScheduleServicesByQuery();

    }
}
