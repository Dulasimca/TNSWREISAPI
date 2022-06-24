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
    public class UpdateEmployeeDetailsController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(EmployeeDetailsEntity EmployeeDetailsEntity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(EmployeeDetailsEntity.Id)));
            sqlParameters.Add(new KeyValuePair<string, string>("@EndDate", Convert.ToString(EmployeeDetailsEntity.EndDate)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Remarks", Convert.ToString(EmployeeDetailsEntity.Remarks)));
            var result = manageSQL.UpdateValues("UpdateEmployeeDetails", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }

    public class EmployeeDetailsEntity
    {
        public int Id { get; set; }
        public string EndDate { get; set; }
        public string Remarks { get; set; }
    }
}
