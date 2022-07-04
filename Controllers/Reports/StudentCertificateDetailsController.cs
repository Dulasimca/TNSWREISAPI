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
    public class StudentCertificateDetailsController : Controller
    {
        [HttpGet("{id}")]
        public string Get(int DCode, int TCode, int HCode)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HCode)));
            ds = manageSQL.GetDataSetValues("GetStudentCertificateDetails", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }

}
