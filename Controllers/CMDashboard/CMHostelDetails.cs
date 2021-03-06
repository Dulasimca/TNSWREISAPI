using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.CMDashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMHostelDetails : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string HostelId,string Month, string Year)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", HostelId));
                sqlParameters.Add(new KeyValuePair<string, string>("@Month", Month));
                sqlParameters.Add(new KeyValuePair<string, string>("@Year", Year));
                var result = manageSQL.GetDataSetValues("GetHostelAlldetails", sqlParameters);
                return JsonConvert.SerializeObject(result);
                //HostelId=55&Month=5&Year=2022
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return "";
            }
        }
    }
}
