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
    public class StudentFacilityController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            ds = manageSQL.GetDataSetValues("GetStudentFacilityMaster");
            return JsonConvert.SerializeObject(ds.Tables[0]);

            //ManageSQLConnection manageSQL = new ManageSQLConnection();
            //var result = manageSQL.GetDataSetValues("GetStudentFacilityMaster");
            //return JsonConvert.SerializeObject(result);
            
        }
        [HttpPost("{id}")]

        public string Post(StudentFacilityEntity StudentFacilityEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@FacilityDetailId", Convert.ToString(StudentFacilityEntity.FacilityDetailId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(StudentFacilityEntity.HostelID)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(StudentFacilityEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(StudentFacilityEntity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@FacilityId", Convert.ToString(StudentFacilityEntity.FacilityId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@NoOfCounts", Convert.ToString(StudentFacilityEntity.NoOfCounts)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Remarks", StudentFacilityEntity.Remarks));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(StudentFacilityEntity.Flag)));
                var result = manageSQL.InsertData("InsertStudentFacility", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
       
    }

    public class StudentFacilityEntity
    {
        public int FacilityDetailId { get; set; }
        public int HostelID { get; set; }
        public int Districtcode { get; set; }
        public int Talukid { get; set; }
        public int FacilityId { get; set; }
        public int NoOfCounts { get; set; }

        public string Remarks { get; set; }

        public bool Flag { get; set; }
    }
}
