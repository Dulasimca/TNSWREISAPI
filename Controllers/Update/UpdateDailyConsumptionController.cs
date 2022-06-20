using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Update
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateDailyConsumptionController : ControllerBase
    {
        [HttpPost("{id}")]
        public bool Post(ConsumptionEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                var result = manageSQL.UpdateValues("DeleteDailyConsumption", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }
    }
    public class ConsumptionEntity
    {
        public Int64 Id { get; set; }
    }
}