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
    public class ApprovalDetailsController : ControllerBase
    {
        [HttpPost("{id}")]

        public string Post(ApprovalDetailsEntity ApprovalDetailsEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(ApprovalDetailsEntity.FacilityDetailId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(ApprovalDetailsEntity.HostelID)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(ApprovalDetailsEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(ApprovalDetailsEntity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ApprovalType", Convert.ToString(ApprovalDetailsEntity.ApprovalType)));
                sqlParameters.Add(new KeyValuePair<string, string>("@RequestId", Convert.ToString(ApprovalDetailsEntity.RequestId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(ApprovalDetailsEntity.Flag)));
                var result = manageSQL.InsertData("InsertApprovalDetails", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]

        public string Get(int DCode, int TCode, int HostelId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HostelId)));
            var result = manageSQL.GetDataSetValues("GetApprovalDetails", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpPut("{id}")]
        public string Put(ApprovalDetailsEntity ApprovalDetailsEntity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(ApprovalDetailsEntity.FacilityDetailId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@ApprovalStatus", Convert.ToString(ApprovalDetailsEntity.ApprovalStatus)));
            var result = manageSQL.UpdateValues("UpdateApprovalDetails", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
    public class ApprovalDetailsEntity
    {
        public int FacilityDetailId { get; set; }
        public int HostelID { get; set; }
        public int Districtcode { get; set; }
        public int Talukid { get; set; }
        public int ApprovalType { get; set; }
        public int RequestId { get; set; }
        public int ApprovalStatus { get; set; }
        public bool Flag { get; set; }
    }
}
