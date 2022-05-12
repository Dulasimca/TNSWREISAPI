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
        public string Get(int Code)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Code ", Convert.ToString(Code)));
                var result = manageSQL.GetDataSetValues("GetHostelAlldetails", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return "";
            }
        }
    }
}
