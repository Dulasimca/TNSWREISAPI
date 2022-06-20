using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardenController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(WardenEntity wardenEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@WardenId", Convert.ToString(wardenEntity.WardenId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Name", wardenEntity.Name));
                sqlParameters.Add(new KeyValuePair<string, string>("@GenderId", wardenEntity.GenderId));
                sqlParameters.Add(new KeyValuePair<string, string>("@DOB", Convert.ToString(wardenEntity.DOB)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Qualification", wardenEntity.Qualification));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelJoinedDate", Convert.ToString(wardenEntity.HostelJoinedDate)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ServiceJoinedDate", Convert.ToString(wardenEntity.ServiceJoinedDate)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Designation", Convert.ToString(wardenEntity.Designation)));
                sqlParameters.Add(new KeyValuePair<string, string>("@EMail", Convert.ToString(wardenEntity.EMail)));
                sqlParameters.Add(new KeyValuePair<string, string>("@PhoneNo", wardenEntity.PhoneNo));
                sqlParameters.Add(new KeyValuePair<string, string>("@AlternateNo", wardenEntity.AlternateNo));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(wardenEntity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Address1", wardenEntity.Address1));
                sqlParameters.Add(new KeyValuePair<string, string>("@Address2",  wardenEntity.Address2));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(wardenEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@WardenImage", Convert.ToString(wardenEntity.WardenImage)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(wardenEntity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Pincode", wardenEntity.Pincode));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(wardenEntity.Flag)));
                var result = manageSQL.InsertData("InsertWarden", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }

        [HttpGet("{id}")]
        public string Get(int DCode, int TCode, int Value)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Value", Convert.ToString(Value)));
            var result = manageSQL.GetDataSetValues("GetWarden", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }

    public class WardenEntity
    {
        public int WardenId { get; set; }
        public string Name { get; set; }
        public string GenderId { get; set; }
        public DateTime DOB { get; set; }
        public string Qualification { get; set; }
        public DateTime ServiceJoinedDate { get; set; }
        public DateTime HostelJoinedDate { get; set; }
        public string EndDate { get; set; }
        public string Designation { get; set; }
        public string EMail { get; set; }
        public string PhoneNo { get; set; }
        public string AlternateNo { get; set; }
        public int HostelId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Districtcode { get; set; }
        public string WardenImage { get; set; }
        
        public int Talukid { get; set; }
        public string Pincode { get; set; }
        public bool Flag { get; set; }
    }
}
