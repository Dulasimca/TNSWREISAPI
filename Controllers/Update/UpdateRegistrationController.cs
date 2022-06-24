using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TNSWREISAPI.ManageSQL;


namespace TNSWREISAPI.Controllers.Update
{
    public class UpdateRegistrationController : Controller
    {
        [HttpPost("{id}")]
        public bool Post(StudentEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", Convert.ToString(entity.studentId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DApproval", Convert.ToString(entity.districtApproval)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TApproval", Convert.ToString(entity.talukApproval)));
                var result = manageSQL.UpdateValues("StudentApproval", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }
    }
    public class StudentEntity
    {
        public Int64 studentId { get; set; }
        public string talukApproval { get; set; }
        public string districtApproval { get; set; }
    }
}
