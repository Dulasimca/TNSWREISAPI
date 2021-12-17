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

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HostelEntity hostelEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Slno", Convert.ToString(hostelEntity.Slno)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelName", hostelEntity.HostelName));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelNameTamil", hostelEntity.HostelNameTamil));
                sqlParameters.Add(new KeyValuePair<string, string>("@HTypeId", Convert.ToString(hostelEntity.HTypeId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(hostelEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(hostelEntity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@BuildingNo", hostelEntity.BuildingNo));
                sqlParameters.Add(new KeyValuePair<string, string>("@Street", hostelEntity.Street));
                sqlParameters.Add(new KeyValuePair<string, string>("@Landmark", hostelEntity.Landmark));
                sqlParameters.Add(new KeyValuePair<string, string>("@Pincode", hostelEntity.Pincode));
                sqlParameters.Add(new KeyValuePair<string, string>("@TotalStudent", hostelEntity.TotalStudent));
                sqlParameters.Add(new KeyValuePair<string, string>("@Phone", hostelEntity.Phone));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelFunctioningType", hostelEntity.HostelFunctioningType));
                sqlParameters.Add(new KeyValuePair<string, string>("@PoliceStationAddress", hostelEntity.PoliceStationAddress));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelOpeningDate", hostelEntity.HostelOpeningDate));
                var result = manageSQL.InsertData("InsertHostelMaster", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }


        [HttpGet("{id}")]

        public string Get(int Type, int DCode, int TCode, int HostelId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Type", Convert.ToString(Type)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HostelId)));
            var result = manageSQL.GetDataSetValues("GetHostelMaster", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }

        [HttpPut("{id}")]
        public string Put(HostelUpdateEntity updateEntity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            //Need to store image
            ImageUpload imageUpload = new ImageUpload();
            var uploadResult = imageUpload.SaveImage(updateEntity.HostelImage._imageAsDataUrl, updateEntity.HostelImage._mimeType, Convert.ToString(updateEntity.HostelId));
            if (uploadResult.Item1)
            {
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(updateEntity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Longitude", updateEntity.Longitude));
                sqlParameters.Add(new KeyValuePair<string, string>("@Latitude", updateEntity.Latitude));
                sqlParameters.Add(new KeyValuePair<string, string>("@Radius", "100"));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelImage", uploadResult.Item2));
                var result = manageSQL.UpdateValues("UpdateHostelMasterById", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            else
            {
                return JsonConvert.SerializeObject("Try Later");
            }
            
        }
    }

    public class HostelUpdateEntity
    {
        public int HostelId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int Radius { get; set; }
        public HostelImageEntity HostelImage { get; set; }
    }

    public class HostelImageEntity
    {
        public string _mimeType { get; set; }
        public string _imageAsDataUrl { get; set; }
    }

    public class HostelEntity
    {
        public int Slno { get; set; }
        public string HostelName { get; set; }
        public string HostelNameTamil { get; set; }
        public int HTypeId { get; set; }
        public int Districtcode { get; set; }
        public int Talukid { get; set; }
        public string BuildingNo { get; set; }
        public string Street { get; set; }
        public string Landmark { get; set; }
        public string Pincode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int Radius { get; set; }
        public string TotalStudent { get; set; }
        public string Phone { get; set; }
        public string HostelImage { get; set; }
        public string PoliceStationAddress { get; set; }
        public string HostelOpeningDate { get; set; }
        public string HostelFunctioningType { get; set; }
    }
}