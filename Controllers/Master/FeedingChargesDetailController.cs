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
    public class FeedingChargesDetailController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(FeedingChargeEntity FeedingChargeEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(FeedingChargeEntity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TypeId", Convert.ToString(FeedingChargeEntity.TypeId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@School", Convert.ToString(FeedingChargeEntity.School)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(FeedingChargeEntity.Flag)));
                sqlParameters.Add(new KeyValuePair<string, string>("@College", Convert.ToString(FeedingChargeEntity.College)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYearId", Convert.ToString(FeedingChargeEntity.AccountingYearId)));
                var result = manageSQL.InsertData("InsertFeedingChargeDetails", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";


        }

        [HttpGet("{id}")]
        public string Get(int AcountingYear)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYearId", Convert.ToString(AcountingYear)));
            var result = manageSQL.GetDataSetValues("GetFeedingChargeDetails", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        public class FeedingChargeEntity
        {
            public int Id { get; set; }
            public int TypeId { get; set; }
            public decimal School { get; set; }
            public bool Flag { get; set; }

            public decimal College { get; set; }

            public int AccountingYearId { get; set; }
        }


    }
}