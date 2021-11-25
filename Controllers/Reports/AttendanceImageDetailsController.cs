using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using Newtonsoft.Json;

namespace TNSWREISAPI.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceImageDetailsController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(int AttendanceId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@ID", Convert.ToString(AttendanceId)));
            var result = manageSQL.GetDataSetValues("GetAttendanceImageById", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
}
