using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using System.Data;

namespace TNSWREISAPI.Controllers.BioMetric
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceBMNameController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string DCode, string TCode, string Adate, string HostelId)
        {
            ManageBioMetricsConnection manageSQL = new ManageBioMetricsConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", DCode));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", TCode));
            sqlParameters.Add(new KeyValuePair<string, string>("@Adate", Adate));
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", HostelId));
            var result = manageSQL.GetDataSetValues("GetBiometricDeviceAttendanceName", sqlParameters);
            return JsonConvert.SerializeObject(result);

        }
    }
}
