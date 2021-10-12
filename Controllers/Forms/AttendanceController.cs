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
    public class AttendanceController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(AttendanceEntity AttendanceEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(AttendanceEntity.AttendId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(AttendanceEntity.HostelID)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(AttendanceEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(AttendanceEntity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AttendanceDate", Convert.ToString(AttendanceEntity.AttendanceDate)));
                sqlParameters.Add(new KeyValuePair<string, string>("@NOOfStudent", Convert.ToString(AttendanceEntity.NOOfStudent)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Remarks", AttendanceEntity.Remarks));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(AttendanceEntity.Flag)));
                var result = manageSQL.InsertData("InsertAttendance", sqlParameters);
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
            var result = manageSQL.GetDataSetValues("GetAttendance");
            return JsonConvert.SerializeObject(result);

        }
    }
    public class AttendanceEntity
    {
      public Int64  AttendId                  {get;set;}
      public int  HostelID              {get;set;}
      public int  Districtcode          {get;set;}
      public int  Talukid               {get;set;}
      public DateTime  AttendanceDate   {get;set;}
     public int NOOfStudent       { get; set; }
     public string  Remarks            {get;set;}
      public bool Flag { get; set; }

    }
}

