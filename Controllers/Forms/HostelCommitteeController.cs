using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelCommitteeController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HostelCommitteeEntity HostelCommitteeEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Slno", Convert.ToString(HostelCommitteeEntity.Slno)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(HostelCommitteeEntity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DistrictId", Convert.ToString(HostelCommitteeEntity.DistrictId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TalukId", Convert.ToString(HostelCommitteeEntity.TalukId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Committee", Convert.ToString(HostelCommitteeEntity.Committee)));
                sqlParameters.Add(new KeyValuePair<string, string>("@CommitteeMembers", Convert.ToString(HostelCommitteeEntity.CommitteeMembers)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Name", Convert.ToString(HostelCommitteeEntity.Name)));
                sqlParameters.Add(new KeyValuePair<string, string>("@MemberName", Convert.ToString(HostelCommitteeEntity.MemberName)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(HostelCommitteeEntity.Flag)));
                var result = manageSQL.InsertData("InsertHostelCommittee", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]
        public string Get(int DCode, int TCode, int HCode)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HCode)));
            var result = manageSQL.GetDataSetValues("GetHostelCommittee", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
    public class HostelCommitteeEntity
    {
        public int Slno { get; set; }
        public int HostelId { get; set; }
        public int DistrictId { get; set; }
        public int TalukId { get; set; }
        public int Committee { get; set; }
        public int CommitteeMembers { get; set; }
        public string Name { get; set; }
        public string MemberName { get; set; }
        public bool Flag { get; set; }
    }
}
