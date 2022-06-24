using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
namespace TNSWREISAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApprovalController : Controller
    {
        [HttpPost("{id}")]
        public bool Post(EmployeeDetailsEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DApproval", Convert.ToString(entity.districtApproval)));
                var result = manageSQL.UpdateValues("UpdateEmployeeApproval", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }
        public class EmployeeDetailsEntity
        {
            public int Id { get; set; }
            public int HostelID { get; set; }
            public int Districtcode { get; set; }
            public int Talukid { get; set; }
            public int Designation { get; set; }
            public int ModeType { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Doj { get; set; }
            public string EmployeeJoinedDate { get; set; }
            public int Gender { get; set; }
            public string Address { get; set; }
            public string NativeDistrict { get; set; }
            public string NativeTaluk { get; set; }
            public string MobileNo { get; set; }
            public string AltMobNo { get; set; }
            public string EndDate { get; set; }
            public string Remarks { get; set; }
            public string Pincode { get; set; }
            public bool Flag { get; set; }
            public string EmployeeImage { get; set; }
            public int districtApproval { get; set; }


        }
    }
}