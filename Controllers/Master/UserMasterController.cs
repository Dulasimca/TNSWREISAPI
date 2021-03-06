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
                Security security = new Security();
                var encryptedValue = security.Encryptword(entity.Pwd);
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                if (entity.Districtcode > 0)
                {
                    sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(entity.Districtcode)));
                }
                if (entity.HostelID > 0)
                {
                    sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(entity.HostelID)));
                }
                if (entity.Talukid > 0)
                {
                    sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(entity.Talukid)));
                }
                sqlParameters.Add(new KeyValuePair<string, string>("@UserName", entity.UserName));
                sqlParameters.Add(new KeyValuePair<string, string>("@RoleId", Convert.ToString(entity.RoleId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@EMailId", entity.EMailId));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Pwd", entity.Pwd));
                sqlParameters.Add(new KeyValuePair<string, string>("@EntryptedPwd", encryptedValue));
                sqlParameters.Add(new KeyValuePair<string, string>("@TasildharId", Convert.ToString(entity.TasildharId)));
                var result = manageSQL.UpdateValues("InsertUserMaster", sqlParameters);
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

        [HttpPut("{id}")]
        public Tuple<bool, string> Put(ChangePasswordEntity entity)
        {
            try
            {
               
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                Security security = new Security();
                var encryptedValue = security.Encryptword(entity.NewPwd);
                var encryptedValue1 = security.Encryptword(entity.OldPwd);
                AuditLog.WriteError(encryptedValue1);
                AuditLog.WriteError(entity.OldEncryptedPwd);
                if (encryptedValue1 == entity.OldEncryptedPwd)
                {
                    List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                    sqlParameters.Add(new KeyValuePair<string, string>("@UserId", Convert.ToString(entity.UserId)));
                    sqlParameters.Add(new KeyValuePair<string, string>("@Newpwd", entity.NewPwd));
                    sqlParameters.Add(new KeyValuePair<string, string>("@NewEncryptedpwd", encryptedValue));
                    var result = manageSQL.UpdateValues("UpdateChangePassword", sqlParameters);
                    return new Tuple<bool, string>(result, "Password has been updated");
 
                } else
                {
                    return new Tuple<bool, string>(false, "Please enter valid current password");
                }
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return new Tuple<bool, string>(false, "Please enter valid input");
            }

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
        public bool Flag { get; set; }
        public string TasildharId { get; set; }
    }

    public class ChangePasswordEntity
    {
        public int UserId { get; set; }
        public string NewPwd { get; set; }
        public string OldPwd { get; set; }
        public string OldEncryptedPwd { get; set; }
    }
}
