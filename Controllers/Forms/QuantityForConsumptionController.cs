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
        public string Get(string Commodity, string AccountingYear, string Date)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Commodity", Commodity));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYear", AccountingYear));
            sqlParameters.Add(new KeyValuePair<string, string>("@Date", Date));
            ds = manageSQL.GetDataSetValues("GetCommodityQtyForConsumption", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

    }
}
