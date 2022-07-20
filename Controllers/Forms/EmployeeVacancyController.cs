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
    public class EmployeeVacancyController : Controller
    {
        [HttpPost("{id}")]
        public bool Post(EmployeeVacancyEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(entity.DCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(entity.TCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(entity.HCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DesignationId", Convert.ToString(entity.DesignationId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@SanctionNo", Convert.ToString(entity.SanctionNo)));
                sqlParameters.Add(new KeyValuePair<string, string>("@FilledNo", Convert.ToString(entity.FilledNo)));
                sqlParameters.Add(new KeyValuePair<string, string>("@VacancyNo", Convert.ToString(entity.VacancyNo)));
                sqlParameters.Add(new KeyValuePair<string, string>("@VacantDate", entity.VacantDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@Reason", entity.Reason));
                return manageSQL.InsertData("InsertIntoEmployeeVacancy", sqlParameters);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return false;
        }

        [HttpGet("{id}")]
        public string Get(int DCode, int TCode, int HCode)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            DataSet ds = new DataSet();
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HCode)));
            ds = manageSQL.GetDataSetValues("GetEmployeeVacancyDetails", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
}
