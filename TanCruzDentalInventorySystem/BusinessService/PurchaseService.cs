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
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IUnitOfWork unitOfWork, IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
            _purchaseRepository.UnitOfWork = unitOfWork;
        }

        public IEnumerable<PurchaseViewModel> GetPurchaseList()
        {
            var purchaseList =  _purchaseRepository.GetPurchaseList();
            return Mapper.Map<List<PurchaseViewModel>>(purchaseList);
        }
    }
}