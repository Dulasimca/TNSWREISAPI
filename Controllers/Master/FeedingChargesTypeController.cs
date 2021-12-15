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
    public class FeedingChargesTypeController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(FeedingChargeTypeEntity FeedingChargeTypeEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@TypeId", Convert.ToString(FeedingChargeTypeEntity.TypeId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Name", Convert.ToString(FeedingChargeTypeEntity.Name)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(FeedingChargeTypeEntity.Flag)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Priorities", Convert.ToString(FeedingChargeTypeEntity.Priorities)));
                var result = manageSQL.InsertData("", sqlParameters);
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
                var result = manageSQL.GetDataSetValues("GetFeedingChargestype");
                return JsonConvert.SerializeObject(result);
            }

        }

        public class FeedingChargeTypeEntity
        {
            public int TypeId { get; set; }
            public string Name { get; set; }
            public bool Flag { get; set; }

            public bool Priorities { get; set; }
        }

    }

