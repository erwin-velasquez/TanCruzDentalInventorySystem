
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public IEnumerable<Purchase> GetPurchaseList()
        {
            List<Purchase> result = new List<Purchase>();
            for (int x = 0; x < 100; x++)
            {
                result.Add(new Purchase()
                {
                    BP_ID = "bp_id " + x.ToString(),
                    CHANGE_DATE = DateTime.UtcNow,
                    CHANGE_ID = "Manglinong, James P.",
                    CREATE_DATE = DateTime.UtcNow,
                    CREATE_ID = "Manglinong, James P.",
                    CURRENCY_ID = "PHP",
                    DELIVERY_DATE = DateTime.UtcNow,
                    DOCUMENT_DATE = DateTime.UtcNow,
                    ID = x,
                    POSTING_DATE = DateTime.UtcNow,
                    PO_CONTROL_NUM = x,
                    PO_DISCOUNT = 10,
                    PO_DISC_AMT = 10,
                    PO_STATUS = "Active",
                    PURCHASEORDER_ID = x.ToString(),
                    REFDOC_NUM = "REFDOC_NUM" + x.ToString(),
                    REMARKS = "Purchase sampling 101. This is a test purchase.",
                    SO_TAX = 1,
                    SO_TOTAL = 100
                }); ; ;
            }
            IEnumerable<Purchase> output = result;
            return output;
        }
    }
}