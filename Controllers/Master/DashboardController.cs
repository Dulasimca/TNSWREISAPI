using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        [HttpGet("{id}")]
        public string Get(int RoleId, int DCode, int TCode, int HCode)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@DistrictCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@RoleId", Convert.ToString(RoleId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TalukId", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(HCode)));
            var result = manageSQL.GetDataSetValues("GetDashboardData", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
}
