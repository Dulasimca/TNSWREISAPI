using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Update
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatePurchaseOrderController : ControllerBase
    {
        [HttpPost("{id}")]
        public bool Post(PurchaseEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.PurchaseId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Type", Convert.ToString(entity.Type)));
                var result = manageSQL.UpdateValues("DeletePuchaseOrder", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }
    }
    public class PurchaseEntity
    {
        public Int64 PurchaseId { get; set; }
        public int Type { get; set; }
    }
    }
