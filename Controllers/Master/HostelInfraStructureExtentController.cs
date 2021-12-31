using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelInfraStructureExtentController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HostelInfraStructureExtentEntity HostelInfraStructureExtentEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(HostelInfraStructureExtentEntity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelInfraStructureId", Convert.ToString(HostelInfraStructureExtentEntity.HostelInfraStructureId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(HostelInfraStructureExtentEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(HostelInfraStructureExtentEntity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(HostelInfraStructureExtentEntity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@FloorNo", Convert.ToString(HostelInfraStructureExtentEntity.FloorNo)));
                sqlParameters.Add(new KeyValuePair<string, string>("@StudentRoom", Convert.ToString(HostelInfraStructureExtentEntity.StudentRoom)));
                sqlParameters.Add(new KeyValuePair<string, string>("@WardenRoom", Convert.ToString(HostelInfraStructureExtentEntity.WardenRoom)));
                sqlParameters.Add(new KeyValuePair<string, string>("@BathRoomNos", Convert.ToString(HostelInfraStructureExtentEntity.BathRoomNos)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ToiletRoomNos", Convert.ToString(HostelInfraStructureExtentEntity.ToiletRoomNos)));
                sqlParameters.Add(new KeyValuePair<string, string>("@UrinalNos", Convert.ToString(HostelInfraStructureExtentEntity.UrinalNos)));
                sqlParameters.Add(new KeyValuePair<string, string>("@StudyingArea", Convert.ToString(HostelInfraStructureExtentEntity.StudyingArea)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(HostelInfraStructureExtentEntity.Flag)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Kitchen", Convert.ToString(HostelInfraStructureExtentEntity.Kitchen)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Library", Convert.ToString(HostelInfraStructureExtentEntity.Library)));
                var result = manageSQL.InsertData("InsertHostelInfraStructureExtent", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";

        }
        [HttpGet("{id}")]
        public string Get(int Districtcode, int Talukid, int HostelId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(Districtcode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(Talukid)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(HostelId)));
            ds = manageSQL.GetDataSetValues("GetHostelInfraStructureExtent", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class HostelInfraStructureExtentEntity
    {
        public int Id { get; set; }

        public int HostelInfraStructureId { get; set; }

        public int Districtcode { get; set; }

        public int Talukid { get; set; }

        public int HostelId { get; set; }

        public string FloorNo { get; set; }

        public string StudentRoom { get; set; }

        public string WardenRoom { get; set; }

        public string BathRoomNos { get; set; }

        public string ToiletRoomNos { get; set; }

        public string UrinalNos { get; set; }

        public string StudyingArea { get; set; }

        public bool Flag { get; set; }

        public string Kitchen { get; set; }

        public string Library { get; set; }

     }
    }
