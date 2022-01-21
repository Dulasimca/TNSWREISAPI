
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
                sqlParameters.Add(new KeyValuePair<string, string>("@TOFundId", Convert.ToString(entity.ToFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DOFundId", Convert.ToString(entity.DoFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccHeadFundId", Convert.ToString(entity.AccHeadFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccHeadId", (entity.AccHeadId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@GroupTypeId", (entity.GroupTypeId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(entity.DCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(entity.TCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TalukAmount", Convert.ToString(entity.TalukAmount)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccYearId", Convert.ToString(entity.YearId)));
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
        public string Get(int AccHeadFundId, int TCode, int Type, int DCode, int AccYearId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AccHeadFundId", Convert.ToString(AccHeadFundId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@YearId", Convert.ToString(AccYearId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Type", Convert.ToString(Type)));
            ds = manageSQL.GetDataSetValues("GetTOFundAllotment", sqlParameters);
            return JsonConvert.SerializeObject(ds);
        }
    }

        public class TOFundAllotmentEntity
        {
            public int ToFundId { get; set; }
            public int DoFundId { get; set; }
            public int AccHeadFundId { get; set; }
            public string AccHeadId { get; set; }
            public string GroupTypeId { get; set; }
            public int DCode { get; set; }
            public int TCode { get; set; }
            public float TalukAmount { get; set; }
            public int YearId { get; set; }
            public bool Flag { get; set; }
        }

}

