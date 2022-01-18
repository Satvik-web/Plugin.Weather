using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Weather
{
    public class Weather
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
        public string Weather_Current { get; set; }
        public string Wind_Speed { get; set; }
        public string Wind_Degree { get; set; }
        public string Wind_Direction { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string Cloud_Cover { get; set; }
        public string IsDay { get; set; }
        public string Observation_Time { get; set; }
        public string Weather_Icon_url { get; set; }
        public void Save_Weather_Icon_As_Png(string path) 
        {
            if (!path.Contains("png") || !path.Contains("PNG")) 
            {
                path = path + ".png";
            }
            File.WriteAllText(path, GetContent().Trim());
        }
        public void Save_Weather_Icon_As_Jpg(string path)
        {
            if (!path.Contains("jpg") || !path.Contains("JPG"))
            {
                path = path + ".jpg";
            }
            File.WriteAllText(path, GetContent().Trim());
        }
        public void Save_Weather_Icon_As_Gif(string path)
        {
            if (!path.Contains("gif") || !path.Contains("GIF"))
            {
                path = path + ".gif";
            }
            File.WriteAllText(path, GetContent().Trim());
        }
        private string GetContent() 
        {
            WebClient client = new WebClient();
            string contents = client.DownloadString(Weather_Icon_url);
            return contents;
        }
    }
}
