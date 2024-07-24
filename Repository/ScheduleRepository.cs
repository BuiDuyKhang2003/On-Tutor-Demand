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

        public void UpdateSchedule(Schedule schedule) => ScheduleDAO.UpdateSchedule(schedule);

        public void DeleteSchedule(int? scheduleId) => ScheduleDAO.DeleteSchedule(scheduleId);

        public IQueryable<Schedule> GetScheduleServicesByQuery() => ScheduleDAO.GetScheduleServicesByQuery();

    }
}
