using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	public interface ISalesOrderRepository
	{
		IUnitOfWork UnitOfWork { get; set; }
		IEnumerable<SalesOrder> GetSalesOrderList();
	}
}
