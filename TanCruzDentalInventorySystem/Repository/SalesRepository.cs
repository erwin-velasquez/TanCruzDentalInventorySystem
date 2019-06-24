
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
    public class SalesRepository : ISalesRepository
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public IEnumerable<Sales> GetSalesList()
        {
            List<Sales> result = new List<Sales>();
            for (int x = 0; x < 100; x++)
            {
                result.Add(new Sales()
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
                    SO_CONTROL_NUM = x,
                    SO_DISCOUNT = 10,
                    SO_DISC_AMT = 10,
                    SO_STATUS = "Active",
                    SALESORDER_ID = x.ToString(),
                    REFDOC_NUM = "REFDOC_NUM" + x.ToString(),
                    REMARKS = "Sales sampling 101. This is a test Sales.",
                    SO_TAX = 1,
                    SO_TOTAL = 100
                }); ; ;
            }
            IEnumerable<Sales> output = result;
            return output;
        }
    }
}