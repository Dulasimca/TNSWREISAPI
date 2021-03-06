using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class TashildarMappingController : Controller
    {
        [HttpPost("{id}")]
        public string Post(SpecialTashildarEntity SpecialTashildarEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(SpecialTashildarEntity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DistrictId", Convert.ToString(SpecialTashildarEntity.DistrictId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TalukId", Convert.ToString(SpecialTashildarEntity.TalukId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TashildarName", Convert.ToString(SpecialTashildarEntity.TashildarName)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(SpecialTashildarEntity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(SpecialTashildarEntity.Flag)));
                var result = manageSQL.InsertData("InsertTashildarMapping", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]
        public string Get(int type, string value)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            string procedure = string.Empty;
            if (type == 1)
            {
                procedure = "GetTalishdarDetailsByEmail";
                sqlParameters.Add(new KeyValuePair<string, string>("@EmailId", value));
            }
            else
            {
                procedure = "GetTashildarMapping";
            }
            ds = manageSQL.GetDataSetValues(procedure, sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        public class SpecialTashildarEntity
        {
            public int Id { get; set; }
            public int DistrictId { get; set; }
            public int TalukId { get; set; }
            public string TashildarName { get; set; }
            public bool Flag { get; set; }
            public int HostelId { get; set; }

        }
        }
}
