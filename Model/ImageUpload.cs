using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TNSWREISAPI.Model
{
    public class ImageUpload
    {

        public Tuple<bool,string> SaveImage(string imageData,string mimeType,string HostelId)
        {
            string nimgData = string.Empty, extn=string.Empty, path=string.Empty, 
                filename=string.Empty, NewfullPath=string.Empty;
            bool isUpload = false;
            try
            {
                // imageData = imageData.Replace("data:image/png;base64,", "");
                nimgData = imageData.Replace("data:image/jpeg;base64,", "").Replace("data:image/png;base64,", "");
                string[] _mimeType = mimeType.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                extn = string.Empty;
                if (_mimeType.Count() > 1)
                {
                    extn = _mimeType[1].ToString();
                }
                path = GlobalVariable.FolderPath+ HostelId;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filename = "webcam_" + HostelId + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + "." + extn;//Server.MapPath("~/UploadWebcamImages/webcam_") + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "_").Replace(":", "") + ".png";
                                                                                                             // var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folder);

                NewfullPath = path + "//" + filename; // Path.Combine(path, filename);
                using (FileStream fs = new FileStream(NewfullPath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(nimgData);
                        bw.Write(data);
                        bw.Close();
                        isUpload = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isUpload = false;
                AuditLog.WriteError(ex.Message);
            }
            return new Tuple<bool, string> (isUpload, filename);
        }
    }
}
