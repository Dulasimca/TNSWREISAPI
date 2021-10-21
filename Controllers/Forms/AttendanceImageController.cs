using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Module;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceImageController : ControllerBase
    {
       [HttpPost("{id}")]
        public Tuple<bool, string> Post(AttendanceImageEntity AttendanceImageEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                /// Need to check conditions
                ImageUpload imageUpload = new ImageUpload();
                var uploadResult = imageUpload.SaveImage(AttendanceImageEntity.uploadImage._imageAsDataUrl, AttendanceImageEntity.uploadImage._mimeType, Convert.ToString(AttendanceImageEntity.HostelID));
                if (uploadResult.Item1)
                {
                    List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                    sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(AttendanceImageEntity.AttendId)));
                    sqlParameters.Add(new KeyValuePair<string, string>("@Uploaddate", Convert.ToString(AttendanceImageEntity.Uploaddate)));
                    sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(AttendanceImageEntity.Districtcode)));
                    sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(AttendanceImageEntity.Talukid)));
                    sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(AttendanceImageEntity.HostelID)));
                    sqlParameters.Add(new KeyValuePair<string, string>("@AttendanceId", Convert.ToString(AttendanceImageEntity.AttendanceId)));
                    sqlParameters.Add(new KeyValuePair<string, string>("@Remarks", AttendanceImageEntity.Remarks));
                    sqlParameters.Add(new KeyValuePair<string, string>("@ImageName", Convert.ToString(uploadResult.Item2)));
                    sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(AttendanceImageEntity.Flag)));
                    var result = manageSQL.InsertData("InsertAttendanceImage", sqlParameters);
                    return new Tuple<bool, string>(result, "Image captured successfully"); 
                }
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return   new Tuple<bool, string>(false, "Please try later");
        }
        [HttpGet("{id}")]
        public string Get(string HostelID, string Districtcode, string Talukid, string FromDate, string Todate)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", HostelID));
            sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Districtcode));
            sqlParameters.Add(new KeyValuePair<string, string>("@FromDate", FromDate));
            sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Talukid));
            sqlParameters.Add(new KeyValuePair<string, string>("@Todate", Todate));
            var result = manageSQL.GetDataSetValues("GetAttendanceImage", sqlParameters);
            return JsonConvert.SerializeObject(result);

        }

    }

    public class AttendanceImageEntity
    {
        public Int64 AttendId { get; set; }
        public string Uploaddate { get; set; }
        public int Districtcode { get; set; }
        public int Talukid { get; set; }
        public int HostelID { get; set; }
        public int AttendanceId { get; set; }
        public string Remarks { get; set; }
        public bool Flag { get; set; }
        public string Latitute { get; set; }
        public string Longitude { get; set; }
        public UploadImageEntity uploadImage { get; set; }



    }

    public class UploadImageEntity
    {
        public string _mimeType { get; set; }
        public string _imageAsDataUrl { get; set; }
    }
}
