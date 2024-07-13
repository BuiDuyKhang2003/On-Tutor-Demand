using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IScheduleRepository
    {
        IQueryable<Schedule> GetScheduleServicesByQuery();
        IEnumerable<Schedule> GetAllSchedules();
        Task<Schedule> GetScheduleById(int? scheduleId);
        void AddSchedule(Schedule schedule);
        void UpdateSchedule(Schedule schedule);
        void DeleteSchedule(int? scheduleId);
    }
}
