using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        [HttpPost("{id}")]
        public Tuple<bool, string> Post(ChangePasswordEntity entity)
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

                }
                else
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
}
