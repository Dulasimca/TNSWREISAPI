
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
    public class FundManagementReportController : Controller
    {
        [HttpGet("{id}")]
        public string Get( int AccHead, int AccountingYear)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AccHead", Convert.ToString(AccHead)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Year", Convert.ToString(AccountingYear)));
            ds = manageSQL.GetDataSetValues("GetFundDetails", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
}
