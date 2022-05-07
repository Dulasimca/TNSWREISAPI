using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.CMDashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMTalukWiseDetails : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(int Code)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@TalukId", Convert.ToString(Code)));
                var result = manageSQL.GetDataSetValues("GetTalukWisedetails", sqlParameters);
                ManageCMDashboardData cMDashboardData = new ManageCMDashboardData();
                var nData = cMDashboardData.ManageDashBoardData(result);
                return JsonConvert.SerializeObject(nData);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return "";
            }
        }
    }
}
