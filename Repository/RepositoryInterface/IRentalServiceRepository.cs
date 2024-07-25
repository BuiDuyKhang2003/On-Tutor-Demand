﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IRentalServiceRepository
    {
        IQueryable<RentalService> GetRentalServicesByQuery();
        IEnumerable<RentalService> GetAllRentalServices();
        RentalService GetRentalServiceById(int? rentalServiceId);
        Task AddRentalService(RentalService rentalService);
        void UpdateRentalService(RentalService rentalService);
        void DeleteRentalService(int? rentalServiceId);
    }
}
