using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using System.Data;

namespace TNSWREISAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceBMController : ControllerBase
    {
       

        [HttpGet("{id}")]
        public string Get(string Adate,string hotelId)
        {
            ManageBioMetricsConnection manageSQL = new ManageBioMetricsConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();           
            sqlParameters.Add(new KeyValuePair<string, string>("@Adate", Adate));
            sqlParameters.Add(new KeyValuePair<string, string>("@hotelId", hotelId));
            var result = manageSQL.GetDataSetValues("GetBiometricDeviceAttendance", sqlParameters);
            return JsonConvert.SerializeObject(result);

        }
    }
  
}

