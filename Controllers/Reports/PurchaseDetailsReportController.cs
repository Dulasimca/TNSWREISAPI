using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDetailsReportController : ControllerBase
    {
        [HttpGet("{id}")]

        public string Get(int DCode, int TCode, int HostelId, string FDate, string TDate)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(HostelId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@FDate", Convert.ToString(FDate)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TDate", Convert.ToString(TDate)));
            var result = manageSQL.GetDataSetValues("GetPurchaseDetailByDate", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
}
