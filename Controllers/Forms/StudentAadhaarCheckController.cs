using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Controllers.Forms;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAadhaarCheckController : Controller
    {
        [HttpGet("{id}")]

        public string Get(string AadharNo, string StudentId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AadharNo", AadharNo));
            sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", StudentId));
            var result = manageSQL.GetDataSetValues("GetStudentCheckAadharNo", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
}
