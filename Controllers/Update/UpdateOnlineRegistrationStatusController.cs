using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.Update
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateOnlineRegistrationStatusController : Controller
    {
        [HttpPost("{id}")]
        public string Post(StudentDetailsEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", Convert.ToString(entity.StudentId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.UpdateValues("UpdateOnlineStudentRegStatus", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }

    public class StudentDetailsEntity
    {
        public int StudentId { get; set; }
        public int Flag { get; set; }
    }
}
