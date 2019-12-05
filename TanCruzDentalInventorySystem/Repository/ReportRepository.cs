using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TanCruzDentalInventorySystem.App_Data;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
    public class ReportRepository : IReportRepository
    {
        public DataSet GetItemsReport()
        {
            InventorySystemDataSet ds = new InventorySystemDataSet();
            var connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("GetItems", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(ds, ds.ItemsDataTable.TableName);

            return ds;
        }


        public DataSet GetSalesOrderReport() {
            InventorySystemDataSet ds = new InventorySystemDataSet();
            var connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("GetSalesOrders", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(ds, ds.SalesOrderDataTable.TableName);

            return ds;
        }

        public DataSet GetPurchaseOrderReport()
        {
            InventorySystemDataSet ds = new InventorySystemDataSet();
            var connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("GetPurchaseOrders", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(ds, ds.PurchaseOrderDataTable.TableName);

            return ds;
        }

        public DataSet GetSalesOrderReceipt()
        {
            InventorySystemDataSet ds = new InventorySystemDataSet();
            var connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("GetSalesOrderReceipt", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(ds, ds.PurchaseOrderDataTable.TableName);

            return ds;
        }
    }
}