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
    public class HostelInfraStructureFloorController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(int Districtcode, int Talukid, int HostelId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(Districtcode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(Talukid)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(HostelId)));
            var result = manageSQL.GetDataSetValues("GetHostelInfraFloorReport", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
}
