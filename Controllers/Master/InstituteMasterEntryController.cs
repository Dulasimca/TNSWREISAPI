using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using System.Data;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituteMasterEntryController : Controller
    {
        [HttpPost("{id}")]
        public string Post(InstituteMasterEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(entity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@IType", Convert.ToString(entity.InstituteType )));
                sqlParameters.Add(new KeyValuePair<string, string>("@InstituteName",entity.InstituteName));
                sqlParameters.Add(new KeyValuePair<string, string>("@Address", entity.Address));
                sqlParameters.Add(new KeyValuePair<string, string>("@InstituteCode ",entity.ICode));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("InsertIntoInstituteMaster", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }

         
    }

    public class InstituteMasterEntity
    {
        public long Id { get; set; }
        public int Districtcode { get; set; }
        public int InstituteType { get; set; }
        public string InstituteName { get; set; }
        public string Address { get; set; }
        public string ICode { get; set; }
        public bool Flag { get; set; }
    }

}
