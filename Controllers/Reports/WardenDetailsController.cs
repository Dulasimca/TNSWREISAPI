using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using System.Data;

namespace TNSWREISAPI.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardenDetailsController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(WardenDetailEntity wardenEntity)
        {
            try
            {
                DataSet ds = new DataSet();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", wardenEntity.Talukid));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(wardenEntity.Districtcode)));
                ds = manageSQL.GetDataSetValues("GetWardenDetails", sqlParameters);
                return JsonConvert.SerializeObject(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
    }

    public class WardenDetailEntity
    {
        public string Talukid { get; set; }
        public string Districtcode { get; set; }

    }

}
