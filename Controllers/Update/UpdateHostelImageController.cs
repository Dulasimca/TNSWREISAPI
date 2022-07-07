using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.Update
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateHostelImageController : Controller
    {
        [HttpPost("{id}")]
        public bool Post(UpdateHostelImageEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(entity.HostelId)));
                var result = manageSQL.UpdateValues("UpdateHostelImage", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }
    }
    public class UpdateHostelImageEntity
    {
        public long HostelId { get; set; }
    }
}
