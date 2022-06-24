
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
    public class SubcasteMasterController : Controller
    {
        [HttpPost("{id}")]
        public string Post(SubcasteMasterEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@SubId", Convert.ToString(entity.SubId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.CasteId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Name", entity.Name));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("InsertSubCasteMaster", sqlParameters);
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
            ds = manageSQL.GetDataSetValues("GetSubCasteMaster");
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class SubcasteMasterEntity
    {
        public int SubId { get; set; }
        public int CasteId { get; set; }
        public string Name { get; set; }
        public bool Flag { get; set; }
    }
}
