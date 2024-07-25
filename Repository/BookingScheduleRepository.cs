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
    public class BookingScheduleRepository : IBookingScheduleRepository
    {
        private readonly BookingScheduleDAO _bookingScheduleDAO;

        public BookingScheduleRepository(AppDbContext dbContext)
        {
            _bookingScheduleDAO = new BookingScheduleDAO(dbContext);
        }

        public async Task<bool> BookScheduleAsync(int scheduleId, int accountId) => await _bookingScheduleDAO.BookScheduleAsync(scheduleId, accountId);

        public async Task<IEnumerable<BookingSchedule>> GetBookingHistoryAsync(int accountId) => await _bookingScheduleDAO.GetBookingHistoryAsync(accountId);
    }
}
