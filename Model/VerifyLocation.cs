using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNSWREISAPI.Model
{
    public class VerifyLocation
    {
        public  double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {


            double latSource = lat1;
            double longSource = lon1;
            double latDestination = lat2;
            double longDestination = lon2;
            string sourceAddress = getAddress(latSource, longSource);
            string destinationAddress = getAddress(latDestination, longDestination);
            string diff = getDistance(sourceAddress, destinationAddress);

            return Convert.ToDouble(diff); 
            //double rlat1 = Math.PI * lat1 / 180;
            //double rlat2 = Math.PI * lat2 / 180;
            //double theta = lon1 - lon2;
            //double rtheta = Math.PI * theta / 180;
            //double dist =
            //    Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
            //    Math.Cos(rlat2) * Math.Cos(rtheta);
            //dist = Math.Acos(dist);
            //dist = dist * 180 / Math.PI;
            //dist = dist * 60 * 1.1515;

            //switch (unit)
            //{
            //    case 'K': //Kilometers -> default
            //        return dist * 1.609344;
            //    case 'N': //Nautical Miles 
            //        return dist * 0.8684;
            //    case 'S': //Nautical Miles 
            //        return dist / 1000;
            //    case 'M': //Miles
            //        return dist;
            //}

            //float DeltaFi = (float)ConvertToRadians(lat2 - lat1);
            //float DeltaLambda = (float)ConvertToRadians(lon2 - lon1);
            //float a = Mathf.Sin(DeltaFi / 2) * Mathf.Sin(DeltaFi / 2) + Mathf.Cos(fi1) * Mathf.Cos(fi2) * Mathf.Sin(DeltaLambda / 2) * Mathf.Sin(DeltaLambda / 2);
            //float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
            //float distance = earthD * c;

            //return dist;
        }

        //private void btnCalculate_Click(object sender, EventArgs e)
        //{
        //    double latSource = Convert.ToDouble(txtLat1.Text.Trim());
        //    double longSource = Convert.ToDouble(txtLong1.Text.Trim());
        //    double latDestination = Convert.ToDouble(txtLat2.Text.Trim());
        //    double longDestination = Convert.ToDouble(txtLong2.Text.Trim());
        //    string sourceAddress = getAddress(latSource, longSource);
        //    string destinationAddress = getAddress(latDestination, longDestination);
        //    txtDistance.Text = getDistance(sourceAddress, destinationAddress);
        //}

        protected string GetJsonData(string url)
        {
            string sContents = string.Empty;
            string me = string.Empty;
            try
            {
                if (url.ToLower().IndexOf("https:") > -1)
                {
                    System.Net.WebClient client = new System.Net.WebClient();
                    byte[] response = client.DownloadData(url);
                    sContents = System.Text.Encoding.ASCII.GetString(response);
                }
                else
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(url);
                    sContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch
            {
                AuditLog.WriteError("GetJsonData :  unable to connect to server ");
                sContents = "unable to connect to server ";
            }
            return sContents;
        }

        public string getAddress(double latitude, double longitude)
        {
            string address = "";
            string content = GetJsonData("https://maps.googleapis.com/maps/api/geocode/json?latlng=" + latitude + "," + longitude + "&sensor=true");
            JObject obj = JObject.Parse(content);
            try
            {
                address = obj.SelectToken("results[0].address_components[3].long_name").ToString();
                return address;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return address;
        }

        public string getDistance(string source, string destination)
        {
            int distance = 0;
            string content = GetJsonData("https://maps.googleapis.com/maps/api/directions/json?origin=" + source + "&destination=" + destination + "&sensor=false");
            JObject obj = JObject.Parse(content);
            try
            {
                distance = (int)obj.SelectToken("routes[0].legs[0].distance.value");
                return (distance / 1000).ToString() + " K.M.";
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return (distance / 1000).ToString();
        }
    }

}
