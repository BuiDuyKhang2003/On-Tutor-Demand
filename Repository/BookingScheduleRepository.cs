﻿using BusinessObject;
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
        public async Task<IEnumerable<BookingSchedule>> GetBookingHistoryAsync(int accountId) => await BookingScheduleDAO.GetBookingHistoryAsync(accountId);

        public async Task<bool> CancelBookingAsync(int scheduleId) => await BookingScheduleDAO.CancelBookingAsync(scheduleId);

        public async Task<bool> BookScheduleAsync(int scheduleId, int accountId) => await BookingScheduleDAO.BookScheduleAsync(scheduleId, accountId);

    }
}
