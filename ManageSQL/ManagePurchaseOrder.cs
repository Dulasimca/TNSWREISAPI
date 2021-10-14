using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.Controllers.Forms;

namespace TNSWREISAPI.ManageSQL
{
    public class ManagePurchaseOrder
    {
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();
        public bool InsertPurchaseOrder(PurchaseEntity entity)
        {
            SqlTransaction objTrans = null;
            using (sqlConnection = new SqlConnection(GlobalVariable.ConnectionString))
            {
                DataSet ds = new DataSet();

                sqlCommand = new SqlCommand();
                try
                {
                    if (sqlConnection.State == 0)
                    {
                        sqlConnection.Open();
                    }
                    objTrans = sqlConnection.BeginTransaction();
                    sqlCommand.Transaction = objTrans;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "InsertIntoPurchaseOrder";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", entity.PurchaseId);
                    sqlCommand.Parameters.AddWithValue("@HostelId", entity.HostelId);
                    sqlCommand.Parameters.AddWithValue("@DistrictCode", entity.DistrictCode);
                    sqlCommand.Parameters.AddWithValue("@TalukId", entity.TalukId);
                    sqlCommand.Parameters.AddWithValue("@BillNo", entity.BillNo);
                    sqlCommand.Parameters.AddWithValue("@BillAmount", entity.BillAmount);
                    sqlCommand.Parameters.AddWithValue("@BillDate", entity.BillDate);
                    sqlCommand.Parameters.AddWithValue("@ShopName", entity.ShopName);
                    sqlCommand.Parameters.AddWithValue("@GSTNo", entity.GSTNo);
                    sqlCommand.Parameters.AddWithValue("@Flag", 1);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();

                    foreach (var item in entity.OrderList)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Dispose();
                        sqlCommand = new SqlCommand();

                        sqlCommand.Transaction = objTrans;
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "InsertIntoPurchaseOrderDetails";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Id", item.DetailId);
                        sqlCommand.Parameters.AddWithValue("@OrderId", item.OrderId);
                        sqlCommand.Parameters.AddWithValue("@CommodityId", item.CommodityId);
                        sqlCommand.Parameters.AddWithValue("@UnitId", item.UnitId);
                        sqlCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                        sqlCommand.Parameters.AddWithValue("@Rate", item.Rate);
                        sqlCommand.Parameters.AddWithValue("@Total", item.Total);
                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();
                    objTrans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    AuditLog.WriteError(ex.Message + " : " + ex.StackTrace);
                    objTrans.Rollback();
                    return false;
                }
                finally
                {
                    sqlConnection.Close();
                    sqlCommand.Dispose();
                    ds.Dispose();
                }
            }
        } 

    }
}
