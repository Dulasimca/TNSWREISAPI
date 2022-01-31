using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class BioMetricHiostelController : ControllerBase
    {       
        [HttpGet("{id}")]
        public string Get(string hostelid)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();         
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", hostelid));
            var result = manageSQL.GetDataSetValues("GetBiometricHostelMapping");
            return JsonConvert.SerializeObject(result);
        }
    }

}

