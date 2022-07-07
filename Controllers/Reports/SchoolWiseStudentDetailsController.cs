using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolWiseStudentDetailsController : ControllerBase
    {  
   [HttpGet("{id}")]
        public string Get(int Type, int DCode, int TCode, int HostelId)
    {
        ManageSQLConnection manageSQL = new ManageSQLConnection();
        DataSet ds = new DataSet();
        List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
        sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
        sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
        sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HostelId)));
        ds = manageSQL.GetDataSetValues("GetSchoolWiseStudentDetails", sqlParameters);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}
}
