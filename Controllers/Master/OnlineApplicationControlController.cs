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
    public class OnlineApplicationControlController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(OnlineApplicationControlEntity OnlineApplicationControlEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(OnlineApplicationControlEntity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelType", Convert.ToString(OnlineApplicationControlEntity.HostelType)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ApplicationType", Convert.ToString(OnlineApplicationControlEntity.ApplicationType)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AcademicYear", Convert.ToString(OnlineApplicationControlEntity.AcademicYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ApplicationOpenDate", Convert.ToString(OnlineApplicationControlEntity.ApplicationOpenDate)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ApplicationCloseDate", Convert.ToString(OnlineApplicationControlEntity.ApplicationCloseDate)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(OnlineApplicationControlEntity.Flag)));
                var result = manageSQL.InsertData("InsertOnlineApplicationControl", sqlParameters);
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
            ds = manageSQL.GetDataSetValues("GetOnlineApplicationDetails");
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
}
        public class OnlineApplicationControlEntity
        {
            public int Id { get; set; }
            public int HostelType { get; set; }
            public int ApplicationType { get; set; }
            public int AcademicYear { get; set; }
            public DateTime ApplicationOpenDate { get; set; }
            public DateTime ApplicationCloseDate { get; set; }
            public bool Flag { get; set; }
        }
