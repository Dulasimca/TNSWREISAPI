using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.BioMetrics
{
    [Route("api/[controller]")]
    [ApiController]
    public class BioMetricsForConsumption : ControllerBase
    {
        [HttpPost("{id}")]
        public int Post(ConsumptionEntityForBio BioMetricEntity)
        {
            string DeviceNumber = string.Empty; int studentCount = 0;
            try
            {
                ManageBioMetricsConnection manageSQL = new ManageBioMetricsConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
               //Gets the Biometric device id based on the hostel
                sqlParameters.Add(new KeyValuePair<string, string>("@ ", Convert.ToString(BioMetricEntity.month)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ ", Convert.ToString(BioMetricEntity.year)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ ", Convert.ToString(BioMetricEntity.bioDate)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ ", Convert.ToString(BioMetricEntity.hostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ ", DeviceNumber));
                var result = manageSQL.GetDataSetValues(" ", sqlParameters);
                return studentCount; //JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return 0;
        }
    }

    public class ConsumptionEntityForBio
    {
        public int month { get; set; }
        public int bioDate { get; set; }
        public int hostelId { get; set; }
        public int year { get; set; }
         
    }
}
