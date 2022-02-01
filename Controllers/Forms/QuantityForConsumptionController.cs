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
    public class QuantityForConsumptionController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string Commodity, string Code, string Date, int Type)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            var procedure = string.Empty;
                sqlParameters.Add(new KeyValuePair<string, string>("@Commodity", Commodity));
            if (Type == 1)
            {
                procedure = "GetOBUptoDay";
                sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Code));
            }
            else
            {
                procedure = "GetCommodityQtyForConsumption";
            }
            sqlParameters.Add(new KeyValuePair<string, string>("@Date", Date));
            ds = manageSQL.GetDataSetValues(procedure, sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

    }
}
