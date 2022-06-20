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

namespace TNSWREISAPI.Controllers.Update
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateHostelController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(HostelUpdateEntity updateEntity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            //Need to store image
            ImageUpload imageUpload = new ImageUpload();
            Tuple<bool, string> uploadResult;
            if (updateEntity.isMobile == 1)
            {
                uploadResult = imageUpload.SaveImage(updateEntity._imageAsDataUrl, updateEntity._mimeType, Convert.ToString(updateEntity.HostelId));
            }
            else
            {
                uploadResult = imageUpload.SaveImage(updateEntity.HostelImage._imageAsDataUrl, updateEntity.HostelImage._mimeType, Convert.ToString(updateEntity.HostelId));
            }
            // var uploadResult = imageUpload.SaveImage(updateEntity.HostelImage._imageAsDataUrl, updateEntity.HostelImage._mimeType, Convert.ToString(updateEntity.HostelId));
            if (uploadResult.Item1)
            {
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(updateEntity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Longitude", updateEntity.Longitude));
                sqlParameters.Add(new KeyValuePair<string, string>("@Latitude", updateEntity.Latitude));
                sqlParameters.Add(new KeyValuePair<string, string>("@Radius", "100"));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelImage", uploadResult.Item2));
                var result = manageSQL.UpdateValues("UpdateHostelMasterById", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            else
            {
                return JsonConvert.SerializeObject("Try Later");
            }

        }
    }
    public class HostelUpdateEntity
    {
        public int HostelId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int Radius { get; set; }
        public HostelImageEntity HostelImage { get; set; }
        public string _mimeType { get; set; }
        public string _imageAsDataUrl { get; set; }
        public int isMobile { get; set; }
    }

    public class HostelImageEntity
    {
        public string _mimeType { get; set; }
        public string _imageAsDataUrl { get; set; }
    }
}
