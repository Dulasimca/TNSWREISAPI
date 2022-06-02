    using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineStudentRegistrationDetailsController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(int DCode, int TCode, int HCode)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HCode)));
            ds = manageSQL.GetDataSetValues("GetOnlineRegistration", sqlParameters);
            GeneratePDFDocument generatePDF = new GeneratePDFDocument();
            generatePDF.Generate(ds);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        [HttpPut("{id}")]
        public bool Put(StudentEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", Convert.ToString(entity.studentId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DApproval", Convert.ToString(entity.districtApproval)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TApproval", Convert.ToString(entity.talukApproval)));
                var result = manageSQL.UpdateValues("OnlineRegStudentApproval", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }
    }
}
