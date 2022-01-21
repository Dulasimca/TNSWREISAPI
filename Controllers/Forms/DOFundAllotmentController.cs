
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
                sqlParameters.Add(new KeyValuePair<string, string>("@DOFundId", Convert.ToString(entity.DOFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccHeadFundId", Convert.ToString(entity.AccHeadFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccHeadId", (entity.AccHeadId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@GroupTypeId",(entity.GroupTypeId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(entity.DCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DistrictAmount", Convert.ToString (entity.DistrictFund)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYearId", Convert.ToString(entity.AccYearId)));

                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("InsertDistrictFundAllotment", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]
        public string Get(int AccHeadFundId, int DCode, int Type, int YearId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AccHeadId", Convert.ToString(AccHeadFundId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Type", Convert.ToString(Type)));
            sqlParameters.Add(new KeyValuePair<string, string>("@YearId", Convert.ToString(YearId)));

            ds = manageSQL.GetDataSetValues("GetDOFundAllotment", sqlParameters);
            return JsonConvert.SerializeObject(ds);
        }

        public class DOFundAllotmentEntity
        {
            public int DOFundId { get; set; }
            public int AccHeadFundId { get; set; }
            public string AccHeadId { get; set; }
            public string GroupTypeId { get; set; }  
            public int DCode { get; set; }
            public int AccYearId { get; set; }

            public float DistrictFund { get; set; }
            public bool Flag { get; set; }
        }

    }
}

