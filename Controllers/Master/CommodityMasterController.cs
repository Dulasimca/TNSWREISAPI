using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommodityMasterController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(CommodityMasterEntity commodityMasterEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(commodityMasterEntity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Name", commodityMasterEntity.Name));
                sqlParameters.Add(new KeyValuePair<string, string>("@NameTamil", commodityMasterEntity.NameTamil));
                sqlParameters.Add(new KeyValuePair<string, string>("@CommodityGroupId", Convert.ToString(commodityMasterEntity.CommodityGroupId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(commodityMasterEntity.Flag)));
                var result = manageSQL.InsertData("InsertCommodityMaster", sqlParameters);
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
            DataSet ds = new DataSet();
            ds = manageSQL.GetDataSetValues("GetCommodityMaster");
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class CommodityMasterEntity
    {
       public int  Id               {get;set;}
       public string  Name          {get;set;}
       public string  NameTamil     {get;set;}
       public int  CommodityGroupId {get;set;} 
       public bool Flag { get; set; }
    }
}
