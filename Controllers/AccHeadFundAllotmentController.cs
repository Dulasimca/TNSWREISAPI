
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
    public class AccHeadFundAllotmentController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HOFundAllotmentEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HOFundId", Convert.ToString(entity.HoFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYearId", Convert.ToString(entity.AccYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@GroupTypeId", entity.GroupTypeId));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccHeadId", entity.AccHeadId));
                sqlParameters.Add(new KeyValuePair<string, string>("@Amount", Convert.ToString(entity.Amount)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("[InsertAccHeadFundAllotment]", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }

        [HttpGet("{id}")]
        public string Get(int AccountingYearId, int Type)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AccYearId", Convert.ToString(AccountingYearId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Type", Convert.ToString(Type)));

            ds = manageSQL.GetDataSetValues("GetAccHeadFundAllotment", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

        public class HOFundAllotmentEntity
        {
            public long Id { get; set; }
            public int HoFundId { get; set; }

            public int AccYear { get; set; }
            public string GroupTypeId { get; set; }
            public string AccHeadId { get; set; }
            public float Amount { get; set; }

            public bool Flag { get; set; }
        }

    }
}

