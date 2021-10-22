using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderDetailsController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(ReportEntity reportEntity)
        {
            try
            {
                DataSet ds = new DataSet();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@TCode", reportEntity.Talukid));
                sqlParameters.Add(new KeyValuePair<string, string>("@DCode", reportEntity.Districtcode));
                sqlParameters.Add(new KeyValuePair<string, string>("@FDate", reportEntity.FromDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@TDate", reportEntity.ToDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", reportEntity.HostelId));
                ds = manageSQL.GetDataSetValues("GetPurchaseOrderByDate", sqlParameters);
                return JsonConvert.SerializeObject(ds);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
    }
}
