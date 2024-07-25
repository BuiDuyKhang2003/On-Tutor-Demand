using BusinessObject;
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
        Task<RentalService> GetRentalServiceById(int? rentalServiceId);
        Task AddRentalService(RentalService rentalService);
        Task<RentalService> UpdateRentalService(RentalService rentalService);
        Task DeleteRentalService(int? rentalServiceId);
    }
}
