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
    public class FoodEntitlementController : ControllerBase
    {
        [HttpPost("{id}")]

        public string Post(FoodEntitlementEntity FoodEntitlementEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@FoodId", Convert.ToString(FoodEntitlementEntity.FoodId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelType", FoodEntitlementEntity.HostelType));
                sqlParameters.Add(new KeyValuePair<string, string>("@Commodity", Convert.ToString(FoodEntitlementEntity.Commodity)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Quantity", Convert.ToString(FoodEntitlementEntity.Quantity)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYearId", Convert.ToString(FoodEntitlementEntity.AccountingYearId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(FoodEntitlementEntity.Flag)));
                var result = manageSQL.InsertData("InsertFoodEntitlement", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]
        public string Get(int AccountingYearId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYearId", Convert.ToString(AccountingYearId)));
            var result = manageSQL.GetDataSetValues("GetFoodEntitlement", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
    public class FoodEntitlementEntity
    {
        public int FoodId { get; set; }
        public string HostelType { get; set; }
        public int Commodity { get; set; }  
        public int Quantity { get; set; }
        public int AccountingYearId { get; set; }
        public bool Flag { get; set; }
    }
}
