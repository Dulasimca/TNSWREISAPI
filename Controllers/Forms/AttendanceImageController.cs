using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;
using System.Data;

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
                Tuple<bool, string> uploadResult;
                /// Need to check conditions
                DataSet ds = new DataSet();
                //List<KeyValuePair<string, string>> sqlParameters1 = new List<KeyValuePair<string, string>>();
                //sqlParameters1.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(AttendanceImageEntity.HostelID)));
                //ds = manageSQL.GetDataSetValues("GetHostelMasterById", sqlParameters1);
                //if(ds.Tables.Count>0) 
                //{
                //    if(ds.Tables[0].Rows.Count>0)
                //    {
                //        double Diffdistance;
                //        double lat1, lon1, lat2, lon2;

                //        VerifyLocation verify = new VerifyLocation();
                //        lat1 = Convert.ToDouble(ds.Tables[0].Rows[0]["Latitude"]);
                //        lon1 = Convert.ToDouble(ds.Tables[0].Rows[0]["Longitude"]);
                //        lat2 = Convert.ToDouble(AttendanceImageEntity.Latitute);
                //        lon2 = Convert.ToDouble(AttendanceImageEntity.Longitude);
                //        Diffdistance =  verify.DistanceTo(lat1, lon1, lat2, lon2);
                //        if(Diffdistance < 10)
                //        {
                //            AuditLog.WriteError("Distance : " + Diffdistance.ToString());
                //            return new Tuple<bool, string>(false, " You have to take a image with in the Hostel location. ");
                //        }
                //        else
                //        {

                //        }
                //    }
                //}

                ImageUpload imageUpload = new ImageUpload();

                if (AttendanceImageEntity.isMobile == 1)
                {
                    uploadResult = imageUpload.SaveImage(AttendanceImageEntity._imageAsDataUrl, AttendanceImageEntity._mimeType, Convert.ToString(AttendanceImageEntity.HostelID));
                }
                else
                {
                    uploadResult = imageUpload.SaveImage(AttendanceImageEntity.uploadImage._imageAsDataUrl, AttendanceImageEntity.uploadImage._mimeType, Convert.ToString(AttendanceImageEntity.HostelID));
                }
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
                    sqlParameters.Add(new KeyValuePair<string, string>("@Longitude", AttendanceImageEntity.Longitude));
                    sqlParameters.Add(new KeyValuePair<string, string>("@Latitude", AttendanceImageEntity.Latitute));
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
        public string _mimeType { get; set; }
        public string _imageAsDataUrl { get; set; }

        public int isMobile { get; set; }


    }

    public class UploadImageEntity
    {
        public string _mimeType { get; set; }
        public string _imageAsDataUrl { get; set; }
    }
}
