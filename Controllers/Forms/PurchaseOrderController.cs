using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        [HttpPost("{id}")]
        public Tuple<bool, DataTable> Post(PurchaseEntity entity)
        {
            if (entity.Type == 1)
            {
                ManagePurchaseOrder managePurchaseOrder = new ManagePurchaseOrder();
                DataTable dt = new DataTable();
                var result = managePurchaseOrder.InsertPurchaseOrder(entity);
                return new Tuple<bool, DataTable>(result, dt);
            }
            else
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                DataSet ds = new DataSet();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@FDate", entity.FDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@TDate", entity.TDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(entity.DistrictCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(entity.TalukId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(entity.HostelId)));
                ds = manageSQL.GetDataSetValues("GetPurchaseOrderByDate", sqlParameters);
                return new Tuple<bool, DataTable>(true, ds.Tables[0]);
            }
        }

        [HttpGet("{id}")]
        public string Get(int OrderId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(OrderId)));
            ds = manageSQL.GetDataSetValues("GetPurchaseOrderById", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }

    public class PurchaseEntity
    {
        public Int64 PurchaseId { get; set; }
        public int HostelId { get; set; }
        public int DistrictCode { get; set; }
        public int TalukId { get; set; }
        public string BillNo { get; set; }
        public decimal BillAmount { get; set; }
        public string BillDate { get; set; }
        public string ShopName { get; set; }
        public string GSTNo { get; set; }
        public int Flag { get; set; }
        public int Type { get; set; }
        public string FDate { get; set; }
        public string TDate { get; set; }
        public List<PurchaseDetailsEntity> OrderList { get; set; }
    }

    public class PurchaseDetailsEntity
    {
        public Int64 DetailId { get; set; }
        public Int64 OrderId { get; set; }
        public int CommodityId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
    }
}