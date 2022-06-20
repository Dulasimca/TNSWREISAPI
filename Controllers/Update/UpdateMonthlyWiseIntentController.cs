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
    public class UpdateMonthlyWiseIntentController : Controller
    {
        [HttpPost("{id}")]
        public string Post(MonthlywiseIntentEntity MonthlywiseIntentEntity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(MonthlywiseIntentEntity.Id)));
            sqlParameters.Add(new KeyValuePair<string, string>("@ApprovalStatus", Convert.ToString(MonthlywiseIntentEntity.ApprovalStatus)));
            var result = manageSQL.UpdateValues("UpdateMonthlywiseIntent", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
    public class MonthlywiseIntentEntity
    {
        public int Id { get; set; }
        public int ApprovalStatus { get; set; }
    }
    }
