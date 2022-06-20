using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Update
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateStudentAttendanceController : Controller
    {
        [HttpPost("{id}")]
        public bool Post(StudentAttendanceEntity entity)
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
    }
    public class StudentAttendanceEntity
    {
        public int MealId { get; set; }
        public bool Status { get; set; }
        public int AttendanceId { get; set; }
        public int TypeId { get; set; }



    }
}
