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
    public class UserMasterController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(UserMasterEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(entity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(entity.HostelID)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(entity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@UserName", entity.UserName));
                sqlParameters.Add(new KeyValuePair<string, string>("@RoleId", Convert.ToString(entity.RoleId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@EMailId", entity.EMailId));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Pwd", entity.Pwd));
                sqlParameters.Add(new KeyValuePair<string, string>("@EntryptedPwd", entity.EntryptedPwd));
                var result = manageSQL.InsertData("InsertUserMaster", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }

        [HttpGet]
        public string Get()
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            var result = manageSQL.GetDataSetValues("GetUserMaster");
            return JsonConvert.SerializeObject(result);
        }
    }
    public class UserMasterEntity
    {
        public Int64 Id { get; set; }
        public int RoleId { get; set; }
        public int Districtcode { get; set; }
        public int Talukid { get; set; }
        public int HostelID { get; set; }
        public string UserName { get; set; }
        public string EMailId { get; set; }
        public string Pwd { get; set; }
        public string EntryptedPwd { get; set; }
        public bool Flag { get; set; }
    }
}
