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
    public class HostelGoController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HostelGoEntity hostelgoEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(hostelgoEntity.Slno)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", hostelgoEntity.HostelID));              
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(hostelgoEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(hostelgoEntity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@GoNumber", hostelgoEntity.GoNo));
                sqlParameters.Add(new KeyValuePair<string, string>("@GoDate", hostelgoEntity.GoDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@AllotmentStudent", hostelgoEntity.TotalStudent));
                sqlParameters.Add(new KeyValuePair<string, string>("@Remarks", Convert.ToString(hostelgoEntity.Remarks)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", hostelgoEntity.flag));
                var result = manageSQL.InsertData("InsertGOMaster", sqlParameters);
                return JsonConvert.SerializeObject(result); 
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }


        [HttpGet("{id}")]
        public string Get(int Type, int RoleId, int DCode, int TCode, int HostelId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Value", Convert.ToString(HostelId)));
            var result = manageSQL.GetDataSetValues("GetGOMaster", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }

    public class HostelGoEntity
    {
        public int Slno { get; set; }
        public string HostelID { get; set; }
        public int Districtcode { get; set; }
        public int Talukid { get; set; }
        public string GoNo { get; set; }
        public string GoDate { get; set; }
        public string Remarks { get; set; }
        public string TotalStudent { get; set; }
        public string flag { get; set; }
        
    }
}

