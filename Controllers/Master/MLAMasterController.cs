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
    public class MLAMasterController : Controller
    {
        [HttpGet("{id}")]
        public string Get(int MPId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@MPId", Convert.ToString(MPId)));
            ds = manageSQL.GetDataSetValues("GetMLAConstituency", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

    }
}
