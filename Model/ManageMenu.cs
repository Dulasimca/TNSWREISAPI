using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace TNSWREISAPI.Model
{
    public class ManageMenu
    {
        /// <summary>
        /// Get the menu tree values
        /// </summary>
        /// <param name="list">list of all menu details</param>
        /// <param name="parent">parent</param>
        /// <returns></returns>
        public List<Menu> GetMenuTree(List<Menu> list, int? parent)
        {
            try
            {
                return list.Where(x => x.parentId == parent).OrderBy(a => a.ID).Select(x => new Menu
                {
                    ID = x.ID,
                    label = x.label,
                    parentId = x.parentId,
                    routerLink = x.routerLink,
                    isActive = x.isActive,
                    icon = x.icon,
                    items = GetMenuTree(list, x.ID)
                }).ToList();
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        public List<Menu> ConvertDataTableToList(DataTable dataTable)
        {
            List<Menu> menus = new List<Menu>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    menus = (from DataRow dr in dataTable.Rows
                             select new Menu()
                             {
                                 ID = Convert.ToInt32(dr["ID"] != null ? dr["ID"] : 0),
                                 label = Convert.ToString(dr["Name"] != null ? dr["Name"] : ""),
                                 parentId = Convert.ToInt32(dr["ParentId"] != null ? dr["ParentId"] : 0),
                                 icon = Convert.ToString(dr["ICon"] != null ? dr["ICon"] : ""),
                                 RoleId = Convert.ToInt32(dr["RoleId"] != null ? dr["RoleId"] : 0),
                                 routerLink = Convert.ToString(dr["URL"] != null ? dr["URL"] : ""),
                                 isActive = Convert.ToBoolean(dr["IsActive"] != null ? dr["IsActive"] : 1)
                             }).ToList();

                }
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }

            return menus;
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
    public class Menu
    {
        public int ID { get; set; }
        public string label { get; set; }
        public string routerLink { get; set; }
        public int? parentId { get; set; }
        public string icon { get; set; }
        public bool isActive { get; set; }
        public int RoleId { get; set; }
        public List<Menu> items { get; set; }
    }
}
