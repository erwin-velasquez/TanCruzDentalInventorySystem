using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem.BusinessService
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _SalesRepository;

        public SalesService(IUnitOfWork unitOfWork, ISalesRepository SalesRepository)
        {
            _SalesRepository = SalesRepository;
            _SalesRepository.UnitOfWork = unitOfWork;
        }

        public IEnumerable<SalesViewModel> GetSalesList()
        {
            var SalesList =  _SalesRepository.GetSalesList();
            return Mapper.Map<List<SalesViewModel>>(SalesList);
        }
    }
}