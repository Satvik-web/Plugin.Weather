using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Plugin.Weather
{
    public class Api
    {
        public static Weather GetWeatherByCity(string city_name) 
        {
            Weather weather = new Weather();
            WebClient webClient = new WebClient();
            string json = webClient.DownloadString($"http://api.weatherstack.com/current?access_key=d555e075b1399158843fd7c98af500ab&query={city_name}").Trim();
            try
            {
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                JsonElement root = jsonDocument.RootElement;
                JsonElement current = root.GetProperty("current");
                JsonElement weather_icon = current.GetProperty("weather_icons");
                JsonElement weather_current = current.GetProperty("weather_descriptions");
                JsonElement location = root.GetProperty("location");
                JsonElement city = location.GetProperty("name");
                JsonElement country = location.GetProperty("country");
                JsonElement state = location.GetProperty("region");
                JsonElement latitude = location.GetProperty("lat");
                JsonElement longitude = location.GetProperty("lon");
                JsonElement timezone = location.GetProperty("timezone_id");
                JsonElement windspeed = current.GetProperty("wind_speed");
                JsonElement winddegree = current.GetProperty("wind_degree");
                JsonElement winddirection = current.GetProperty("wind_dir");
                JsonElement pressure = current.GetProperty("pressure");
                JsonElement humidity = current.GetProperty("humidity");
                JsonElement cloudcover = current.GetProperty("cloudcover");
                JsonElement isday = current.GetProperty("is_day");
                JsonElement observationtime = current.GetProperty("observation_time");
                string tmpweather = weather_current.GetRawText();
                tmpweather = tmpweather.Trim();
                tmpweather = tmpweather.Replace("[", "");
                tmpweather = tmpweather.Replace("]", "");
                tmpweather = tmpweather.Replace("\"", "");
                weather.Weather_Current = tmpweather;
                weather.City = city.GetString();
                weather.Country = country.GetString();
                weather.State = state.GetString();
                weather.Latitude = latitude.GetString();
                weather.Longitude = longitude.GetString();
                weather.TimeZone = timezone.GetString();
                weather.Wind_Speed = windspeed.GetDouble().ToString();
                weather.Wind_Degree = winddegree.GetDouble().ToString();
                weather.Wind_Direction = winddirection.GetString();
                weather.Pressure = pressure.GetDouble().ToString();
                weather.Humidity = humidity.GetDouble().ToString();
                weather.Cloud_Cover = cloudcover.GetDouble().ToString();
                weather.IsDay = isday.GetString();
                weather.Observation_Time = observationtime.GetString();
                string tmpiconurl = weather_icon.GetRawText();
                tmpiconurl = tmpiconurl.Replace("[", "");
                tmpiconurl = tmpiconurl.Replace("]", "");
                tmpiconurl = tmpiconurl.Replace("\\", "");
                tmpiconurl = tmpiconurl.Replace("\"", "");
                weather.Weather_Icon_url = tmpiconurl;
            }
            catch (Exception ex) 
            {
                if (json.Trim().Equals("{\"success\":false,\"error\":{\"code\":601,\"type\":\"missing_query\",\"info\":\"Please specify a valid location identifier using the query parameter.\"}}")) 
                {
                    throw new Exception("Please Specify a valid City!");
                }
            }
            return weather;
        }

        public static string Get_Curent_Season_Northern_Hemisphere_As_String()
        {
            DateTime now = DateTime.Now;
            string month = now.ToString("MMM");
            month = month.ToLower();
            string season = null;
            if (month.Equals("dec") || month.Equals("jan") || month.Equals("feb")) 
            {
                season = "Winter";
            }
            if (month.Equals("mar") || month.Equals("apr") || month.Equals("may"))
            {
                season = "Spring";
            }
            if (month.Equals("jun") || month.Equals("jul") || month.Equals("aug"))
            {
                season = "Summer";
            }
            if (month.Equals("sep") || month.Equals("oct") || month.Equals("nov"))
            {
                season = "Autumn";
            }
            return season;
        }
        public static string Get_Curent_Season_Southern_Hemisphere_As_String()
        {
            DateTime now = DateTime.Now;
            string month = now.ToString("MMM");
            month = month.ToLower();
            string season = null;
            if (month.Equals("dec") || month.Equals("jan") || month.Equals("feb"))
            {
                season = "Summer";
            }
            if (month.Equals("mar") || month.Equals("apr") || month.Equals("may"))
            {
                season = "Autumn";
            }
            if (month.Equals("jun") || month.Equals("jul") || month.Equals("aug"))
            {
                season = "Winter";
            }
            if (month.Equals("sep") || month.Equals("oct") || month.Equals("nov"))
            {
                season = "Spring";
            }
            return season;
        }
        public static Image Get_Curent_Season_Image_Northern_Hemisphere(int val) 
        {
            DateTime now = DateTime.Now;
            string month = now.ToString("MMM");
            month = month.ToLower();
            Image season = null;
            if (month.Equals("dec") || month.Equals("jan") || month.Equals("feb"))
            {
                season = GetWinterImage(val);
            }
            if (month.Equals("mar") || month.Equals("apr") || month.Equals("may"))
            {
                season = GetSpringImage(val);
            }
            if (month.Equals("jun") || month.Equals("jul") || month.Equals("aug"))
            {
                season = GetSummerImage(val);
            }
            if (month.Equals("sep") || month.Equals("oct") || month.Equals("nov"))
            {
                season = GetAutumnImage(val);
            }
            return season;
        }
        public static Image Get_Curent_Season_Image_Southern_Hemisphere(int val) 
        {
            DateTime now = DateTime.Now;
            string month = now.ToString("MMM");
            month = month.ToLower();
            Image season = null;
            if (month.Equals("dec") || month.Equals("jan") || month.Equals("feb"))
            {
                season = GetSummerImage(val);
            }
            if (month.Equals("mar") || month.Equals("apr") || month.Equals("may"))
            {
                season = GetAutumnImage(val);
            }
            if (month.Equals("jun") || month.Equals("jul") || month.Equals("aug"))
            {
                season = GetWinterImage(val);
            }
            if (month.Equals("sep") || month.Equals("oct") || month.Equals("nov"))
            {
                season = GetSpringImage(val);
            }
            return season;
        }
        public static Image GetSummerImage(int val)
        {
            if (val == 5)
            {
                Image image = GetImageByName("Summer5");
                return image;
            }
            if (val == 4)
            {
                Image image = GetImageByName("Summer4");
                return image;
            }
            if (val == 3)
            {
                Image image = GetImageByName("Summer3");
                return image;
            }
            if (val == 2)
            {
                Image image = GetImageByName("Summer2");
                return image;
            }
            if (val == 1)
            {
                Image image = GetImageByName("Summer1");
                return image;
            }
            else
            {
                return null;
            }
        }
        public static Image GetAutumnImage(int val)
        {
            if (val == 5)
            {
                Image image = GetImageByName("Autumn5");
                return image;
            }
            if (val == 4)
            {
                Image image = GetImageByName("Autumn4");
                return image;
            }
            if (val == 3)
            {
                Image image = GetImageByName("Autumn3");
                return image;
            }
            if (val == 2)
            {
                Image image = GetImageByName("Autumn2");
                return image;
            }
            if (val == 1)
            {
                Image image = GetImageByName("Autumn1");
                return image;
            }
            else
            {
                return null;
            }
        }
        public static Image GetWinterImage(int val) 
        {
            if (val == 5)
            {
                Image image = GetImageByName("Winter5");
                return image;
            }
            if (val == 4)
            {
                Image image = GetImageByName("Winter4");
                return image;
            }
            if (val == 3)
            {
                Image image = GetImageByName("Winter3");
                return image;
            }
            if (val == 2)
            {
                Image image = GetImageByName("Winter2");
                return image;
            }
            if (val == 1)
            {
                Image image = GetImageByName("Winter1");
                return image;
            }
            else 
            {
                return null;
            }
        }
        public static Image GetSpringImage(int val)
        {
            if (val == 5)
            {
                Image image = GetImageByName("Spring5");
                return image;
            }
            if (val == 4)
            {
                Image image = GetImageByName("Spring4");
                return image;
            }
            if (val == 3)
            {
                Image image = GetImageByName("Spring3");
                return image;
            }
            if (val == 2)
            {
                Image image = GetImageByName("Spring2");
                return image;
            }
            if (val == 1)
            {
                Image image = GetImageByName("Spring1");
                return image;
            }
            else
            {
                return null;
            }
        }
        private static Bitmap GetImageByName(string imageName)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);
            return (Bitmap)rm.GetObject(imageName);
        }
    }
}
