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
    public class AttendanceImageController : ControllerBase
    {
       [HttpPost("{id}")]
        public string Post(AttendanceImageEntity AttendanceImageEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(AttendanceImageEntity.AttendId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Uploaddate", Convert.ToString(AttendanceImageEntity.Uploaddate)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(AttendanceImageEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(AttendanceImageEntity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(AttendanceImageEntity.HostelID)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AttendanceId", Convert.ToString(AttendanceImageEntity.AttendanceId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Remarks", AttendanceImageEntity.Remarks));
                sqlParameters.Add(new KeyValuePair<string, string>("@ImageName", Convert.ToString(AttendanceImageEntity.ImageName)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(AttendanceImageEntity.Flag)));
                var result = manageSQL.InsertData("InsertAttendanceImage", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
    }
    public class AttendanceImageEntity
    {
        public Int64 AttendId { get; set; }
        public DateTime Uploaddate { get; set; }
        public int Districtcode { get; set; }
        public int Talukid { get; set; }
        public int HostelID { get; set; }
        public int AttendanceId { get; set; }
        public string Remarks { get; set; }
        public string ImageName { get; set; }
        public bool Flag { get; set; }

    }
}
