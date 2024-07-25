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
    public class RentalServiceRepository : IRentalServiceRepository
    {
        public async Task AddRentalService(RentalService rentalService) => await RentalServiceDAO.AddRentalService(rentalService);

        public async Task DeleteRentalService(int? rentalServiceId) => await RentalServiceDAO.DeleteRentalService(rentalServiceId);

        public async Task<RentalService> GetRentalServiceById(int? rentalServiceId) => await RentalServiceDAO.GetRentalServiceById(rentalServiceId);

        public IEnumerable<RentalService> GetAllRentalServices() => RentalServiceDAO.GetAllRentalServices();

        public async Task<RentalService> UpdateRentalService(RentalService rentalService) => await RentalServiceDAO.UpdateRentalService(rentalService);

        public IQueryable<RentalService> GetRentalServicesByQuery() => RentalServiceDAO.GetRentalServicesByQuery();



    }
}
