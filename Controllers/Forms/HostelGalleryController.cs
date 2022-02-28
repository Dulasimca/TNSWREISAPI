using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelGalleryController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HostelGalleryEntity Entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(Entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccYear", Convert.ToString(Entity.AccYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ImageDescription", Entity.ImageTitle));
                sqlParameters.Add(new KeyValuePair<string, string>("@Image", Entity.Image));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(Entity.Flag)));
                var result = manageSQL.InsertData("InsertHostelGalleryUpload", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }

        [HttpGet("{id}")]
        public string Get(int HCode)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(HCode)));
            DataSet ds = new DataSet();
            var result = manageSQL.GetDataSetValues("GetHostelGallery", sqlParameters);
            return JsonConvert.SerializeObject(result);

        }
    }
    public class HostelGalleryEntity
    {
        public int Id { get; set; }
        public int AccYear { get; set; }
        public int DCode { get; set; }
        public int TCode { get; set; }
        public int HCode { get; set; }
        public string ImageTitle { get; set; }
        public string Image { get; set; }
        public bool Flag { get; set; }
    }
}
