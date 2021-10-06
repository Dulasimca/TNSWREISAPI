using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

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
                sqlParameters.Add(new KeyValuePair<string, string>("@Longitude", hostelEntity.Longitude));
                sqlParameters.Add(new KeyValuePair<string, string>("@Latitude", hostelEntity.Latitude));
                sqlParameters.Add(new KeyValuePair<string, string>("@Radius", Convert.ToString(hostelEntity.Radius)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TotalStudent", hostelEntity.TotalStudent));
                sqlParameters.Add(new KeyValuePair<string, string>("@Phone", hostelEntity.Phone));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelImage", hostelEntity.HostelImage));
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
        public string Get(int Type, int Id)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Type", Convert.ToString(Type)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(Id)));
            var result = manageSQL.GetDataSetValues("GetHostelMaster", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }

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
}
