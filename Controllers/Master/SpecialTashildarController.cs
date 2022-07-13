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
    public class SpecialTashildarController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(SpecialTashildarEntity SpecialTashildarEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Slno", Convert.ToString(SpecialTashildarEntity.Slno)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DistrictId", Convert.ToString(SpecialTashildarEntity.DistrictId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TalukId", Convert.ToString(SpecialTashildarEntity.TalukId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@SplTashildarName", Convert.ToString(SpecialTashildarEntity.SplTashildarName)));
                sqlParameters.Add(new KeyValuePair<string, string>("@MobileNum", Convert.ToString(SpecialTashildarEntity.MobileNum)));
                sqlParameters.Add(new KeyValuePair<string, string>("@EmailId", Convert.ToString(SpecialTashildarEntity.EmailId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(SpecialTashildarEntity.Flag)));
                var result = manageSQL.InsertData("InsertSpecialTashildarMaster", sqlParameters);
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
            ds = manageSQL.GetDataSetValues("GetSpecialTashildarMaster");
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class SpecialTashildarEntity
    {
        public int Slno { get; set; }
        public int DistrictId { get; set; }
        public int TalukId { get; set; }
        public string SplTashildarName { get; set; }
        public string MobileNum { get; set; }
        public string EmailId { get; set; }
        public bool Flag { get; set; }

    }
    }
