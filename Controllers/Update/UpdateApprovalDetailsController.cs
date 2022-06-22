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
    public class UpdateApprovalDetailsController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(ApprovalDetailsEntity ApprovalDetailsEntity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(ApprovalDetailsEntity.FacilityDetailId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@ApprovalStatus", Convert.ToString(ApprovalDetailsEntity.ApprovalStatus)));
            var result = manageSQL.UpdateValues("UpdateApprovalDetails", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
    public class ApprovalDetailsEntity
    {
        public int FacilityDetailId { get; set; }
        public int ApprovalStatus { get; set; }
    }
    }
