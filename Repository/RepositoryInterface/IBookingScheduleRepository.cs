using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IBookingScheduleRepository
    {
        Task<IEnumerable<BookingSchedule>> GetBookingHistoryAsync(int accountId);
        Task<bool> BookScheduleAsync(int scheduleId, int accountId);
        Task<bool> CancelBookingAsync(int bookingId);
    }
}
