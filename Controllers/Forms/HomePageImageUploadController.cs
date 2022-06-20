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

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageImageUploadController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HomePageImageEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@ImageId", Convert.ToString(entity.ImageId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@UploadDate", Convert.ToString(entity.UploadDate)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Title",entity.ImageTitle));
                sqlParameters.Add(new KeyValuePair<string, string>("@FileName",entity.ImageFilename));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("InsertHomePageImage", sqlParameters);
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
            ds = manageSQL.GetDataSetValues("GetHomePageImage");
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }

    public class HomePageImageEntity
    {
        public long ImageId { get; set; }
        public string UploadDate { get; set; }
        public string ImageTitle { get; set; }
        public string ImageFilename { get; set; }
        public bool Flag { get; set; }
    }

}
