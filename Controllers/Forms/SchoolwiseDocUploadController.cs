using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolwiseDocUploadController : Controller
    {
        [HttpPost("{id}")]
        public string Post(SchoolwiseDocEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(entity.DCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(entity.HCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@InstituteId", Convert.ToString(entity.ICode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Filename",entity.Filename));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("InsertSchoolwiseDocumentUpload", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }

        [HttpGet]
        public string Get()
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            ds = manageSQL.GetDataSetValues("GetSchoolwiseDocumentUpload");
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class SchoolwiseDocEntity
    {
        public int Id { get; set; }
        public int DCode { get; set; }
        public int HCode { get; set; }
        public int ICode { get; set; }
        public string Filename { get; set; }
        public bool Flag { get; set; }
    }
}
