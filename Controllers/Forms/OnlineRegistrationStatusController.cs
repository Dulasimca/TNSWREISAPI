using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineRegistrationStatusController : Controller
    {
        [HttpGet("{id}")]
        public string Get(int DStatus)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@DistrictApproval", Convert.ToString(DStatus)));
            ds = manageSQL.GetDataSetValues("GetOnlineRegistrationByStatus", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
}
