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
        public bool Post(PurchaseEntity entity)
        {
            ManagePurchaseOrder managePurchaseOrder = new ManagePurchaseOrder();
            var result = managePurchaseOrder.InsertPurchaseOrder(entity);
            return result;
        }

        [HttpGet("{id}")]
        public string Get(string Value)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Date", Value));
            var result = manageSQL.GetDataSetValues("GetPurchaseOrderByDate", sqlParameters);
            return JsonConvert.SerializeObject(result);
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