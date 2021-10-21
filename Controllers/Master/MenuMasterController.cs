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

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuMasterController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(int roleId)
        {
            ManageSQLConnection manageSQLConnection = new ManageSQLConnection();
            List<KeyValuePair<string, string>> parameterList = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> parameterValues = new KeyValuePair<string, string>();
            ManageMenu manageMenu = new ManageMenu();
            DataSet ds = new DataSet();
            try
            {
                parameterValues = new KeyValuePair<string, string>("@RoleId", Convert.ToString(roleId));
                parameterList.Add(parameterValues);
                ds = manageSQLConnection.GetDataSetValues("GetMenuMaster", parameterList);
                List<Menu> menus = new List<Menu>();
                menus = manageMenu.ConvertDataTableToList(ds.Tables[0]);
                var reult = manageMenu.GetMenuTree(menus, 0);
                return JsonConvert.SerializeObject(reult);
            }
            finally
            {
                ds.Dispose();
                parameterList = null;
                manageMenu = null;
            }
        }


        [HttpPost("{id}")]
        public bool Post(MenuMasterEntity entity)
        {
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            sqlParameters.Add(new KeyValuePair<string, string>("@MenuId", Convert.ToString(entity.MenuId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@ID", Convert.ToString(entity.ID)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Name", entity.Name));
            sqlParameters.Add(new KeyValuePair<string, string>("@URL", entity.URL));
            sqlParameters.Add(new KeyValuePair<string, string>("@ParentId", Convert.ToString(entity.ParentId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@IsActive", Convert.ToString(entity.IsActive)));
            sqlParameters.Add(new KeyValuePair<string, string>("@RoleId", Convert.ToString(entity.RoleId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@ICon", Convert.ToString(entity.ICon)));
            return manageSQL.InsertData("InsertMenuMaster", sqlParameters);
        }

    }
    public class MenuMasterEntity
    {
        public int MenuId { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int ParentId { get; set; }
        public int IsActive { get; set; }
        public int RoleId { get; set; }
        public string ICon { get; set; }
    }
}

