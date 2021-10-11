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
        public class TalukMasterController : ControllerBase
        {
            [HttpPost("{id}")]
            public string Post(TalukEntity talukEntity)
            {
                try
                {
                    ManageSQLConnection manageSQL = new ManageSQLConnection();
                    List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                    sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(talukEntity.Talukid)));
                    sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(talukEntity.Districtcode)));
                    sqlParameters.Add(new KeyValuePair<string, string>("@Talukcode", talukEntity.Talukcode));
                    sqlParameters.Add(new KeyValuePair<string, string>("@Talukname", talukEntity.Talukname));
                    sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(talukEntity.Flag)));
                var result = manageSQL.InsertData("InsertTalukMaster", sqlParameters);
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
                var result = manageSQL.GetDataSetValues("GetTalukMaster");
                return JsonConvert.SerializeObject(result);
            }
        }

        public class TalukEntity
        {
            public int Talukid { get; set; }
            public string Talukcode { get; set; }
            public int  Districtcode { get; set; }
            public string Talukname { get; set; }
            public bool Flag { get; set; }
    }
    }

