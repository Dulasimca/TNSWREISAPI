using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace TNSWREISAPI.Model
{
    public class ManageCMDashboardData
    {

        public List<CMDasshboardEntity> ManageDashBoardData(DataSet ds)
        {
            List<CMDasshboardEntity> _DashBoardData = new List<CMDasshboardEntity>();

            try
            {
                if (ds.Tables.Count > 1)
                {
                    foreach (DataRow ManageData in ds.Tables[1].Rows) // District/Taluk/Hostel
                    {

                        DataRow[] FilteredData = ds.Tables[0].Select("Code='" + Convert.ToString(ManageData["Code"]) + "'");
                        if (FilteredData.Length >= 1)
                        {
                            CMDasshboardEntity _Data = new CMDasshboardEntity();
                            _Data.name = Convert.ToString(FilteredData[0]["Name"]);
                            _Data.Id = Convert.ToInt32(FilteredData[0]["Code"]);
                            foreach (DataRow nFData in FilteredData)
                            {
                                _Data.hcount += Convert.ToInt32(nFData["HCount"]);
                                _Data.genderType = Convert.ToInt32(nFData["HGenderType"]);
                                if (Convert.ToInt32(nFData["HGenderType"]) == 1) //Boys
                                {
                                    _Data.boysHostelCount = Convert.ToInt32(nFData["HCount"]);
                                    _Data.sanctionedBoysCount = Convert.ToInt32(nFData["sanctionedStength"]);
                                    _Data.boysCount = Convert.ToInt32(nFData["StudentCount"]);
                                }
                                else if (Convert.ToInt32(nFData["HGenderType"]) == 2) //Girls
                                {
                                    _Data.girlsHostelCount = Convert.ToInt32(nFData["HCount"]);
                                    _Data.sanctionedGirlsCount = Convert.ToInt32(nFData["sanctionedStength"]);
                                    _Data.girlsCount = Convert.ToInt32(nFData["StudentCount"]);
                                }
                                else //Others
                                {
                                    _Data.boysHostelCount = Convert.ToInt32(nFData["HCount"]);
                                    _Data.sanctionedBoysCount = Convert.ToInt32(nFData["sanctionedStength"]);
                                    _Data.boysCount = Convert.ToInt32(nFData["StudentCount"]);
                                }
                            }
                            _DashBoardData.Add(_Data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return _DashBoardData;
        }
    }

    public class CMDasshboardEntity
    {
        public string name { get; set; }
        public int Id { get; set; }
        public int hcount { get; set; }
        public int boysHostelCount { get; set; }
        public int girlsHostelCount { get; set; }
        public int sanctionedBoysCount { get; set; }
        public int sanctionedGirlsCount { get; set; }
        public int boysCount { get; set; }
        public int girlsCount { get; set; }
        public int genderType { get; set; }
    }
}
