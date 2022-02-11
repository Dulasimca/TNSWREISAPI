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

namespace TNSWREISAPI.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        [HttpPost("{id}")]

        public string Post(FeedBackEntity FeedBackEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Slno", Convert.ToString(FeedBackEntity.Slno)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(FeedBackEntity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DistrictId", Convert.ToString(FeedBackEntity.DistrictId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TalukId", Convert.ToString(FeedBackEntity.TalukId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", Convert.ToString(FeedBackEntity.StudentId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@FBMessage", Convert.ToString(FeedBackEntity.FBMessage)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ImgFileName", FeedBackEntity.ImgFileName));
                sqlParameters.Add(new KeyValuePair<string, string>("@ReplyMessage", FeedBackEntity.ReplyMessage));
                sqlParameters.Add(new KeyValuePair<string, string>("@ActionDate", FeedBackEntity.ActionDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(FeedBackEntity.Flag)));
                var result = manageSQL.InsertData("InsertFeedBack", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }

    }

    public class FeedBackEntity
        {
            public int Slno { get; set; }
            public int HostelId { get; set; }
            public int DistrictId { get; set; }
            public int TalukId { get; set; }
            public int StudentId { get; set; }
            public string FBMessage { get; set; }
            public string ImgFileName { get; set; }
            public string ReplyMessage { get; set; }
            public string ActionDate { get; set; }
            public bool Flag { get; set; }


        }
    }

