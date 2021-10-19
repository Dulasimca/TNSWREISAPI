using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictMasterController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(DistrictMastereEntity DistrictEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(DistrictEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtname", DistrictEntity.Districtname));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(DistrictEntity.Flag)));
                var result = manageSQL.InsertData("InsertDistrictMaster", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet]
        public string Get()
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            var result = manageSQL.GetDataSetValues("GetDistrictMaster");
            return JsonConvert.SerializeObject(result);
        }


    }
    public class DistrictMastereEntity
    {
        public int Districtcode         {get;set;}
       public string  Districtname      {get;set;}
       public bool Flag { get; set; }
    }
}
