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
        public void AddRentalService(RentalService rentalService) => RentalServiceDAO.AddRentalService(rentalService);

        public void DeleteRentalService(int? rentalServiceId) => RentalServiceDAO.DeleteRentalService(rentalServiceId);

        public RentalService GetRentalServiceById(int? rentalServiceId) => RentalServiceDAO.GetRentalServiceById(rentalServiceId);

        public IEnumerable<RentalService> GetAllRentalServices() => RentalServiceDAO.GetAllRentalServices();

        public void UpdateRentalService(RentalService rentalService) => RentalServiceDAO.UpdateRentalService(rentalService);
    }
}
