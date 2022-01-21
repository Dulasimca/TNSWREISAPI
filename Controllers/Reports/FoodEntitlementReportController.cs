using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodEntitlementReportController : ControllerBase
    {
        [HttpGet("{id}")]

        public string Get(int HostelType, int AccountingYearId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelType", Convert.ToString(HostelType)));
            sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYearId", Convert.ToString(AccountingYearId)));
            var result = manageSQL.GetDataSetValues("GetFoodEntitlementreport", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
}
