﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelInfraStructureController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HostelInfraStructureEntity HostelInfraStructureEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(HostelInfraStructureEntity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(HostelInfraStructureEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(HostelInfraStructureEntity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(HostelInfraStructureEntity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TotalArea", Convert.ToString(HostelInfraStructureEntity.TotalArea)));
                sqlParameters.Add(new KeyValuePair<string, string>("@BuildingArea", Convert.ToString(HostelInfraStructureEntity.BuildingArea)));
                sqlParameters.Add(new KeyValuePair<string, string>("@NoOfFloor", Convert.ToString(HostelInfraStructureEntity.NoOfFloor)));
                sqlParameters.Add(new KeyValuePair<string, string>("@NoOfRoom", Convert.ToString(HostelInfraStructureEntity.NoOfRoom)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Kitchen", Convert.ToString(HostelInfraStructureEntity.Kitchen)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Bathroom", Convert.ToString(HostelInfraStructureEntity.Bathroom)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(HostelInfraStructureEntity.Flag)));
                var result = manageSQL.InsertData("InsertHostelInfraStructure", sqlParameters);
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
            var result = manageSQL.GetDataSetValues("GetHostelInfraStructure");
            return JsonConvert.SerializeObject(result);
        }
    }
    public class HostelInfraStructureEntity
    {
        public int Id { get; set; }
        public int Districtcode { get; set; }
        public int Talukid { get; set; }
        public int HostelId { get; set; }
        public string TotalArea { get; set; }
        public string BuildingArea { get; set; }
        public string NoOfFloor { get; set; }
        public string NoOfRoom { get; set; }
        public string Kitchen { get; set; }
        public string Bathroom { get; set; }
        public bool Flag { get; set; }
    }
}
