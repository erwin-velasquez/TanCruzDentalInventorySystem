
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
            result.Add(new Purchase() { BP_ID = "1", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "James" });
            result.Add(new Purchase() { BP_ID = "2", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "Test 2" });
            result.Add(new Purchase() { BP_ID = "3", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "Test 3" });
            result.Add(new Purchase() { BP_ID = "4", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "Test 4" });
            result.Add(new Purchase() { BP_ID = "5", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "Test 5" });
            result.Add(new Purchase() { BP_ID = "6", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "Test 6" });
            result.Add(new Purchase() { BP_ID = "7", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "Test 7" });
            result.Add(new Purchase() { BP_ID = "8", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "Test 8" });
            result.Add(new Purchase() { BP_ID = "9", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "Test 9" });
            result.Add(new Purchase() { BP_ID = "10", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "Test 10" });
            result.Add(new Purchase() { BP_ID = "11", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "Test 11" });
            result.Add(new Purchase() { BP_ID = "12", CHANGE_DATE = DateTime.UtcNow, CREATE_DATE = DateTime.UtcNow, REMARKS = "Test 12" });
            IEnumerable<Purchase> output = result;
            return output;
        }
	}
}