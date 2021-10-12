using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using TNSWREISAPI.ManageSQL;
using Newtonsoft.Json;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("{id}")]
        public Tuple<bool, string, DataTable> Post(LoginEntity entity)
        {
            try
            {
                DataSet ds = new DataSet();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                Security security = new Security();
                var encryptedValue = security.Encryptword(entity.Password);
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@EMailId", entity.UserId));
                ds = manageSQL.GetDataSetValues("GetUserMasterById", sqlParameters);

                if (ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count>0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["EntryptedPwd"]) == encryptedValue) 
                        {
                            return new Tuple<bool, string, DataTable>(true, "Login Successfully", ds.Tables[0]);
                        }
                        else
                        {
                            return new Tuple<bool, string, DataTable>(false, "Password Incorrect, Please enter correct Password", null);

                        }
                    }
                    else
                    {
                        return new Tuple<bool, string, DataTable>(false, "Invalid User Id", null);
                    }
                }
                else
                {
                    return new Tuple<bool, string, DataTable>(false, "Invalid User Id", null);
                }

            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return new Tuple<bool, string, DataTable>(false, "Please enter valid credentials", null);

        }
        public class LoginEntity
        {
            public string UserId { get; set; }
            public string Password { get; set; }
        }
    }
}
