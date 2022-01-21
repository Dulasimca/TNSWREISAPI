
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
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelFundId", Convert.ToString(entity.HosteFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TOFundId", Convert.ToString(entity.ToFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccHeadFundId", Convert.ToString(entity.AccHeadFundId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccHeadId", (entity.AccHeadId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@GroupTypeId", (entity.GroupTypeId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(entity.DCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(entity.TCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId ", Convert.ToString(entity.HCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelFund", entity.HostelAmount));
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
        public string Get(int AccHeadFundId, int HCode, int Type)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AccHeadFundId", Convert.ToString(AccHeadFundId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Type", Convert.ToString(Type)));
            ds = manageSQL.GetDataSetValues("GetHostelFundAllotment", sqlParameters);
            return JsonConvert.SerializeObject(ds);
        }
    }

        public class HostelFundAllotmentEntity
        {
            public int HosteFundId { get; set; }
            public int ToFundId { get; set; }
            public int AccHeadFundId { get; set; }
            public string AccHeadId { get; set; }
            public string GroupTypeId { get; set; }
            public int DCode { get; set; }
            public int TCode { get; set; }
            public int HCode { get; set; }
            public string HostelAmount { get; set; }
            public bool Flag { get; set; }
    }
}

