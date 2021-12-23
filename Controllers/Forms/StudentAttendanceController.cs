
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
    public class StudentAttendanceController : Controller
    {
        [HttpPost("{id}")]
        public string Post(StudentAttendanceEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@MealstypeId", Convert.ToString(entity.MealId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(entity.HCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(entity.DCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TalukCode", Convert.ToString(entity.TCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AttendanceDate", entity.AttendanceDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@isSubmitted  ", Convert.ToString(entity.SubmitStatus)));
                var result = manageSQL.InsertData("InsertStudentAttendance", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]
        public string Get(int MealsTypeId, int HCode, int TCode, int DCode, string AttendanceDate)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@MealstypeId", Convert.ToString(MealsTypeId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(HCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TalukCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@AttendanceDate", Convert.ToString(AttendanceDate)));

            ds = manageSQL.GetDataSetValues("GetStudentAttendance", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        [HttpPut("{id}")]
        public bool Put(StudentAttendanceEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@StudentAttendnceId", Convert.ToString(entity.AttendanceId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@MealstypeId", Convert.ToString(entity.MealId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AttendanceStatus", Convert.ToString(entity.Status)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Type", Convert.ToString(entity.TypeId)));

                var result = manageSQL.UpdateValues("UpdateStudentAttendance", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }
        }
        public class StudentAttendanceEntity
        {
            public int MealId { get; set; }
            public int DCode { get; set; }
            public int TCode { get; set; }
            public int HCode { get; set; }
            public string AttendanceDate { get; set; }
            public bool Status { get; set; }
            public int AttendanceId { get; set; }
            public int TypeId { get; set; }

            public bool SubmitStatus { get; set; }

        }


    }
}
