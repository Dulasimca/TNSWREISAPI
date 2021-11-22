using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using System.Data;

namespace TNSWREISAPI.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpeningBalanceDetailsController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(OpeningBalanceEntity entity)
        {
            try
            {
                DataSet ds = new DataSet();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", entity.Talukid));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", entity.Districtcode));
			    sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", entity.HostelId));   
			    sqlParameters.Add(new KeyValuePair<string, string>("@ShortYear", entity.ShortYear));   
                ds = manageSQL.GetDataSetValues("GetOpeningBalanceDetails", sqlParameters);
                return JsonConvert.SerializeObject(ds);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
    }

    public class OpeningBalanceEntity
    {
        public string Talukid { get; set; }
        public string Districtcode { get; set; }
		public string HostelId { get; set; }
		public string ShortYear{get; set;}

    }

}
