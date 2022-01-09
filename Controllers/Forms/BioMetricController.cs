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
    public class BioMetricController : ControllerBase
    {
        [HttpPost("{id}")]

        public string Post(BioMetricEntity BioMetricEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Slno", Convert.ToString(BioMetricEntity.Slno)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DeviceId", Convert.ToString(BioMetricEntity.DeviceId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(BioMetricEntity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(BioMetricEntity.Flag)));
                var result = manageSQL.InsertData("InsertBiometricDeviceMapping", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]
        public string Get()
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            var result = manageSQL.GetDataSetValues("GetBiometricDeviceMapping");
            return JsonConvert.SerializeObject(result);
        }
    }

      


    public class BioMetricEntity
    {
        public int Slno { get; set; }
        public int DeviceId { get; set; }
        public int HostelId { get; set; }
        public bool Flag { get; set; }
    }
}

