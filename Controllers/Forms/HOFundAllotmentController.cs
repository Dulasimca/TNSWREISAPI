 
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
    public class HOFundAllotmentController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HOFundAllotmentEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@HOFundId", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYearId", Convert.ToString(entity.AccYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@GONumber", Convert.ToString(entity.GoNumber)));
                sqlParameters.Add(new KeyValuePair<string, string>("@GODate", entity.GoDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@BudjetAmount", entity.BudjetAmount));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("InsertHoFundAllotment", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }


        public class HOFundAllotmentEntity
        {
            public long Id { get; set; }
            public int AccYear { get; set; }
            public int GoNumber { get; set; }
            public string GoDate { get; set; }
            public string BudjetAmount { get; set; }
            public bool Flag { get; set; }
        }

    }
}

