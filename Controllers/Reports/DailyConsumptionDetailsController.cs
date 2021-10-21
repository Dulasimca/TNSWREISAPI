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
    public class DailyConsumptionDetailsController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(ReportEntity reportEntity)
        {
            try
            {
                DataSet ds = new DataSet();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", reportEntity.Talukid));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode",  reportEntity.Districtcode));
                sqlParameters.Add(new KeyValuePair<string, string>("@FromDate", reportEntity.FromDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@ToDate", reportEntity.ToDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", reportEntity.HostelId));
                ds = manageSQL.GetDataSetValues("GetDailyConsumptionDetails", sqlParameters);
                return JsonConvert.SerializeObject(ds);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
    }

    public class ReportEntity
    {
        public string Talukid { get; set; }
        public string Districtcode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string HostelId { get; set; }
    }
}
