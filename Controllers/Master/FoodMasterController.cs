using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodMasterController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(FoodEntity foodEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Slno", Convert.ToString(foodEntity.Slno)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HTypeId", Convert.ToString(foodEntity.HTypeId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DayId", Convert.ToString(foodEntity.DayId)));  
                sqlParameters.Add(new KeyValuePair<string, string>("@Breakfast", foodEntity.Breakfast));
                sqlParameters.Add(new KeyValuePair<string, string>("@Lunch", foodEntity.Lunch));
                sqlParameters.Add(new KeyValuePair<string, string>("@Snacks", foodEntity.Snacks));
                sqlParameters.Add(new KeyValuePair<string, string>("@Dinner", foodEntity.Dinner));
                var result = manageSQL.InsertData("InsertFoodMaster", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }

        [HttpGet("{id}")]
        public string Get(int HTypeId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@HTypeId", Convert.ToString(HTypeId)));
            var result = manageSQL.GetDataSetValues("GetFoodMaster", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }

    public class FoodEntity
    {
        public int Slno { get; set; }
        public int HTypeId { get; set; }
        public int DayId { get; set; }
        public string Breakfast { get; set; }
        public string Lunch { get; set; }
        public string Snacks { get; set; }
        public string Dinner { get; set; }
    }
}
