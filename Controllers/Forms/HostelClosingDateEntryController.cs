using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using Newtonsoft.Json;
using System.Data;


namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelClosingDateEntryController : Controller
    {
        [HttpPost("{id}")]
        public string Post(DateEntryEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AppOpenDate", entity.AppOpenDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@AppCloseDate", entity.AppCloseDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@AppExtendDate", entity.AppExtendDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@HstlOpenDate", entity.HstlOpenDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@HstlCloseDate ", entity.HstlCloseDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@AcademicYear", Convert.ToString(entity.AcademicYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));

                var result = manageSQL.InsertData("InsertHostelClosingEntry", sqlParameters);
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
            DataSet ds = new DataSet();
            ds = manageSQL.GetDataSetValues("GetHostelClosingDateEntry");
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

    }
}
public class DateEntryEntity
{
    public int Id { get; set; }
    public string AppOpenDate { get; set; }
    public string AppCloseDate { get; set; }
    public string AppExtendDate { get; set; }
    public string HstlOpenDate { get; set; }
    public string HstlCloseDate { get; set; }
    public int AcademicYear { get; set; }

    public bool Flag { get; set; }

}