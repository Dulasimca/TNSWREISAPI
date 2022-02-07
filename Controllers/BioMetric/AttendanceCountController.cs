using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.BioMetric
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceCountController : ControllerBase
    {

        [HttpGet("{id}")]
        public string Get(string serialno, string month, string year, string  Hcode)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@serialno", serialno));
            sqlParameters.Add(new KeyValuePair<string, string>("@month", month));
            sqlParameters.Add(new KeyValuePair<string, string>("@year", year));
            sqlParameters.Add(new KeyValuePair<string, string>("@Hcode", Hcode));
            var result = manageSQL.GetDataSetValuesBM("GetBDAttendancecount", sqlParameters);
            return JsonConvert.SerializeObject(result);

        }
    }
}
