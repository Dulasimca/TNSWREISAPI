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

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailsController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(EmployeeDetailsEntity EmployeeDetailsEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(EmployeeDetailsEntity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(EmployeeDetailsEntity.HostelID)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(EmployeeDetailsEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(EmployeeDetailsEntity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Designation", Convert.ToString(EmployeeDetailsEntity.Designation)));
                sqlParameters.Add(new KeyValuePair<string, string>("@FirstName", Convert.ToString(EmployeeDetailsEntity.FirstName)));
                sqlParameters.Add(new KeyValuePair<string, string>("@LastName", Convert.ToString(EmployeeDetailsEntity.LastName)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Doj", Convert.ToString(EmployeeDetailsEntity.Doj)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Gender", Convert.ToString(EmployeeDetailsEntity.Gender)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Address", Convert.ToString(EmployeeDetailsEntity.Address)));
                sqlParameters.Add(new KeyValuePair<string, string>("@NativeDistrict", Convert.ToString(EmployeeDetailsEntity.NativeDistrict)));
                sqlParameters.Add(new KeyValuePair<string, string>("@MobileNo", Convert.ToString(EmployeeDetailsEntity.MobileNo)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(EmployeeDetailsEntity.Flag)));
                var result = manageSQL.InsertData("InsertEmployeeDetails", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]

        public string Get(int DCode, int TCode, int HostelId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HostelId)));
            var result = manageSQL.GetDataSetValues("GetEmployeeDetails", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }


    public class EmployeeDetailsEntity
    {
        public int Id { get; set; }
        public int HostelID { get; set; }
        public int Districtcode { get; set; }
        public int Talukid { get; set; }
        public int Designation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Doj { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string NativeDistrict { get; set; }
        public string MobileNo { get; set; }
        public bool Flag { get; set; }
        
    }
}