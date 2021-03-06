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
    public class MonthlywiseIntentController : ControllerBase
    {

        [HttpPost("{id}")]
        public string Post(MonthlywiseIntentEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(entity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(entity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccountingId", Convert.ToString(entity.AccountingId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@CommodityId", Convert.ToString(entity.CommodityId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@UnitId", Convert.ToString(entity.UnitId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Qty", Convert.ToString(entity.Qty)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(entity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@MonthwiseDate", Convert.ToString(entity.MonthwiseDate)));
                var result = manageSQL.InsertData("InsertMonthlywiseIntent", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]
        public string Get(int Districtcode, int Talukid, int HostelId, int AccountingId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(Districtcode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(Talukid)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(HostelId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@AccountingId", Convert.ToString(AccountingId)));
            ds = manageSQL.GetDataSetValues("GetMonthlywiseIntent", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class MonthlywiseIntentEntity
    {
        public int Id { get; set; }
        public int Districtcode { get; set; }
        public int Talukid { get; set; }
        public int HostelId { get; set; }
        public int AccountingId { get; set; }
        public int CommodityId { get; set; }
        public int UnitId { get; set; }
        public decimal Qty { get; set; }
        public bool Flag { get; set; }
        public int ApprovalStatus { get; set; }
        public string MonthwiseDate { get; set; }


    }
}
