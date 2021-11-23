using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDocumentUploadController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(PurchaseUploadEntity purchaseEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
               
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(purchaseEntity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@OrderId", Convert.ToString(purchaseEntity.OrderId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@BillFileName", purchaseEntity.BillFileName));
                sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(purchaseEntity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(purchaseEntity.DistrictCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(purchaseEntity.TalukCode)));
                var result = manageSQL.InsertData("UploadPurchaseOrderDocuments", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]

        public string Get(int HostelId, string BillNo)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(HostelId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@BillNo", BillNo));
              ds = manageSQL.GetDataSetValues("GetPurchaseOrderByBillNo", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
        public class PurchaseUploadEntity
        {
            public int Id { get; set; }
            public int OrderId { get; set; }
            public string BillFileName { get; set; }
            public int HostelId { get; set; }
            public int DistrictCode { get; set; }
            public int TalukCode { get; set; }

        }
    }

