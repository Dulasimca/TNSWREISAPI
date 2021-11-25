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
    public class GOMasterController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(GOMasterEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(entity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(entity.HostelID)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(entity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@GoNumber",  entity.GoNumber));
                sqlParameters.Add(new KeyValuePair<string, string>("@GoDate", Convert.ToString(entity.GoDate)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AllotmentStudent", Convert.ToString(entity.AllotmentStudent)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Remarks", entity.Remarks));
                var result = manageSQL.InsertData("InsertGOMaster", sqlParameters);
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
            var result = manageSQL.GetDataSetValues("GetGOMaster");
            return JsonConvert.SerializeObject(result);
        }
    }

    public class GOMasterEntity
    {
     public long   Id                   {get;set;}
     public int   HostelID              {get;set;}
     public int Districtcode { get;set;}
     public int Talukid { get;set;}
     public string   GoNumber           {get;set;}
     public string   GoDate           {get;set;}
     public int   AllotmentStudent      {get;set;}
     public string Remarks              {get;set;}
     public bool Flag { get; set; }
    }


}
