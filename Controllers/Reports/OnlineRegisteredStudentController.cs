using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using Newtonsoft.Json;

namespace TNSWREISAPI.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineRegisteredStudentController : Controller
    {
        [HttpGet("{id}")]
        public string Get(int HCode, int DCode, int TCode, int RoleId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Roleid", Convert.ToString(RoleId)));

            ds = manageSQL.GetDataSetValues("GetOnlineRegisteredStudent", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
}
