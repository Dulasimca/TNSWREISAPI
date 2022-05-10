using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.CMDashBoard
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMMainDashBoardController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                var result = manageSQL.GetDataSetValues("GetOveralldetails");
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
