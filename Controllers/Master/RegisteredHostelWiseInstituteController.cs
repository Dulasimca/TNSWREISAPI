using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using Newtonsoft.Json;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredHostelWiseInstituteController : Controller
    {
        
            [HttpGet("{id}")]
            public string Get(int HCode)
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                DataSet ds = new DataSet();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HCode)));
                ds = manageSQL.GetDataSetValues("GetOnlineRegistrationByIType", sqlParameters);
                return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
}
