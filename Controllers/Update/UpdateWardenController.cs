using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Update
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateWardenController : Controller
    {
        [HttpPost("{id}")]
        public bool Post(WardenEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Date", entity.EndDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.WardenId)));
                var result = manageSQL.UpdateValues("UpdateWardenDetails", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }
    }
    public class WardenEntity
    {
        public int WardenId { get; set; }
        public string EndDate { get; set; }

    }
    }
