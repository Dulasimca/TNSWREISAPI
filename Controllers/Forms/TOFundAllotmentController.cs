
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
    public class TOFundAllotmentController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(TOFundAllotmentEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@TOFundId", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DOFundId", Convert.ToString(entity.DoFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYearId", Convert.ToString(entity.AccYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(entity.DCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(entity.TCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TOBudjetAmount", entity.TOBudjetAmount));
                  sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("InsertToFundAllotment", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]
        public string Get(int YearId, int TCode)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@YearId", Convert.ToString(YearId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            ds = manageSQL.GetDataSetValues("GetTOFundAllotment", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

        public class TOFundAllotmentEntity
        {
            public int Id { get; set; }
            public int DoFundId { get; set; }
            public int AccYear { get; set; }
            public int DCode { get; set; }
            public int TCode { get; set; }
            public string TOBudjetAmount { get; set; }
            public bool Flag { get; set; }
        }

    }
}

