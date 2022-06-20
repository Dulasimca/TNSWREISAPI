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
    public class HostelFunctioningTypeController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HostelFunctioningTypeEntity HostelFunctioningEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(HostelFunctioningEntity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Name", HostelFunctioningEntity.Name));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(HostelFunctioningEntity.Flag)));
                var result = manageSQL.InsertData("InsertHostelFunctioningType", sqlParameters);
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
            var result = manageSQL.GetDataSetValues("GetHostelFunctioningType");
            return JsonConvert.SerializeObject(result);
        }


    }
    public class HostelFunctioningTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Flag { get; set; }
    }
}
