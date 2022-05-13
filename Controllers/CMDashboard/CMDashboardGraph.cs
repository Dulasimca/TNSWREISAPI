using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.CMDashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMDashboardGraphController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            List<object> list = new List<object>();
            ManageSQLConnection manageSQLConnection = new ManageSQLConnection();
            DataSet ds = new DataSet();
            try
            {
               ds = manageSQLConnection.GetDataSetValues("DashBordEmployeeGraph");
                if (ds.Tables.Count > 1)
                {
                    string[] districtInfo = ds.Tables[1].Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
                    #region Get's the Rice information
                    string[] designationInfo = ds.Tables[2].Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
                    list.Add(designationInfo);
                    list.Add(districtInfo);
                    foreach (var DesignationName in designationInfo)
                    {
                        list.Add(GetValueInArray(DesignationName, ds.Tables[0], districtInfo));
                    }
                    #endregion
                }
            }
            finally
            {
                ds = null;
            }
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// Gets Particular rows in a table.
        /// </summary>
        /// <param name="RegionName">Region Name</param>
        /// <param name="MajorItemName">Major Item Name</param>
        /// <param name="dataTable">Data table value</param>
        /// <returns></returns>
        public decimal[] GetValueInArray(string DesignationName, DataTable dataTable, string[] Regioninfo)
        {
            decimal[] svalues = new decimal[Regioninfo.Length];
            int i = 0;
            foreach (var item in Regioninfo)
            {
                DataRow[] rowsFiltered = dataTable.Select("Districtname='" + item + "' and DesignationName='" + DesignationName + "'");
                //FilterDataRow(item, "PALMOLIEN POUCH", dataTable);
                //ds.Tables[0].Select("RGNAME='" + item + "' and MajorName='RAW GRADEA'");
                if (rowsFiltered != null)
                {
                    if (rowsFiltered.Length > 0)
                    {
                        svalues[i] = Convert.ToDecimal(rowsFiltered[0][2]);
                    }
                    else
                    {
                        svalues[i] = 0;
                    }
                }
                else
                {
                    svalues[i] = 0;
                }
                i++;
            }
            return svalues;
        }

    }
}
