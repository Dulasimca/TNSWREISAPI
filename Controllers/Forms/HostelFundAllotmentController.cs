
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
    public class HostelFundAllotmentController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HostelFundAllotmentEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelFundId", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TOFundId", Convert.ToString(entity.ToFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYearId", Convert.ToString(entity.AccYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(entity.DCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(entity.TCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId ", Convert.ToString(entity.HCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelBudjetAmount", entity.HostelBudjetAmount));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("InsertHostelFundAllotment", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]
        public string Get(int YearId, int HCode, int Type)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@YearId", Convert.ToString(YearId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Type", Convert.ToString(Type)));
            ds = manageSQL.GetDataSetValues("GetHostelFundAllotment", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

        public class HostelFundAllotmentEntity
        {
            public int Id { get; set; }
            public int ToFundId { get; set; }
            public int AccYear { get; set; }
            public int DCode { get; set; }
            public int TCode { get; set; }
            public int HCode { get; set; }
            public string HostelBudjetAmount { get; set; }
            public bool Flag { get; set; }
        }

    }
}

