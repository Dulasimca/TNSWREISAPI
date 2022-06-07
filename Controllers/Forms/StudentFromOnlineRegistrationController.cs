using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using Newtonsoft.Json;
using System.Data;


namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentFromOnlineRegistrationController : Controller
    {
        [HttpPost("{id}")]
        public string Post(StudentFromOnlineRegEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@wardenapproval", Convert.ToString(entity.wardenapproval)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtapproval", Convert.ToString(entity.Districtapproval)));
                var result = manageSQL.InsertData("InsertStudentFromOnlineRegistration", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
    }
    public class StudentFromOnlineRegEntity
    {
        public int Id { get; set; }
        public int wardenapproval { get; set; }
        public int Districtapproval { get; set; }

    }
}
