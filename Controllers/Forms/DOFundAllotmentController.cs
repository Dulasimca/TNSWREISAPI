
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
    public class DOFundAllotmentController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(DOFundAllotmentEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@DOFundId", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HOFundId", Convert.ToString(entity.HoFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYearId", Convert.ToString(entity.AccYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(entity.DCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DOBudjetAmount",entity.DOBudjetAmount));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("InsertDoFundAllotment", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]
        public string Get(int YearId, int DCode, int Type)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@YearId", Convert.ToString(YearId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Type", Convert.ToString(Type)));
            ds = manageSQL.GetDataSetValues("GetDOFundAllotment", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

        public class DOFundAllotmentEntity
        {
            public int Id { get; set; }
            public int HoFundId { get; set; }
            public int AccYear { get; set; }
            public int DCode { get; set; }
            public string DOBudjetAmount { get; set; }
            public bool Flag { get; set; }
        }

    }
}

