using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WUnderground.Api.Data
{
    public class WUndergroundResponse
    {

        private string _version;
        private string _termOfService;

        public string Version
        {
            get
            {
                return _version;
            }

            internal set
            {
                _version = value;
            }
        }

        public string TermOfService { 

            get
            {
                return _termOfService;
            } 

            internal set
            {
                _termOfService = value;
            }
        }

    }

    public class WUndergroundConditionsResponse : WUndergroundResponse
    {
        private string _imageUrl;
        public string ImageUrl
        {

            get
            {
                return _imageUrl;
            }

            internal set
            {
                _imageUrl = value;
            }
        }

        private string _stationId;
        public string StationId
        {
            get
            {
                return _stationId;
            }
            
            internal set
            {
                _stationId = value;
            }
        }

        private string _weather;
        public string Weather
        {
            get
            {
                return _weather;
            }

            internal set
            {
                _weather = value;
            }
        }

        private Int32 _temperatureF;
        public Int32 TemperatureF
        {
            get
            {
                return _temperatureF;
            }

            internal set
            {
                _temperatureF = value;
            }
        }

        private Int32 _temperatureC;
        public Int32 TemperatureC
        {
            get
            {
                return _temperatureC;
            }

            internal set
            {
                _temperatureC = value;
            }
        }

        private string _relativeHumidity;
        public string RelativeHumidity
        {
            get
            {
                return _relativeHumidity;
            }

            internal set
            {
                _relativeHumidity = value;
            }
        }

        private string _windDirection;
        public string WindDirection
        {
            get
            {
                return _windDirection;
            }

            internal set
            {
                _windDirection = value;
            }
        }

        private Int16 _windDegrees;
        public Int16 WindDegrees
        {
            get
            {
                return _windDegrees;
            }

            internal set
            {
                _windDegrees = value;
            }
        }

        private Int32 _windSpeed_Mph;
        public Int32 WindSpeed_Mph
        {
            get
            {
                return _windSpeed_Mph;
            }

            internal set
            {
                _windSpeed_Mph = value;
            }
        }

        private Int32 _windGust_Mph;
        public Int32 WindGust_Mph
        {
            get
            {
                return _windGust_Mph;
            }

            internal set
            {
                _windGust_Mph = value;
            }
        }

        private Int32 _windSpeed_Kph;
        public Int32 WindSpeed_Kph
        {
            get
            {
                return _windSpeed_Kph;
            }

            internal set
            {
                _windSpeed_Kph = value;
            }
        }

        private Int32 _windGust_Kph;
        public Int32 WindGust_Kph
        {
            get
            {
                return _windGust_Kph;
            }

            internal set
            {
                _windGust_Kph = value;
            }
        }

        private Int32 _pressure_mb;
        public Int32 Pressure_mb
        {
            get
            {
                return _pressure_mb;
            }

            internal set
            {
                _pressure_mb = value;
            }
        }

        private Double _pressure_in;
        public Double Pressure_in
        {
            get
            {
                return _pressure_in;
            }

            internal set
            {
                _pressure_in = value;
            }
        }

        private string _pressureTrend;
        public string PressureTrend
        {
            get
            {
                return _pressureTrend;
            }

            internal set
            {
                _pressureTrend = value;
            }
        }

        private Int32 _dewPointC;
        public Int32 DewPointC
        {
            get
            {
                return _dewPointC;
            }

            internal set
            {
                _dewPointC = value;
            }
        }

        private Int32 _dewPointF;
        public Int32 DewPointF
        {
            get
            {
                return _dewPointF;
            }

            internal set
            {
                _dewPointF = value;
            }
        }

        private Double _visibility_mi;
        public Double VisibilityMi
        {
            get
            {
                return _visibility_mi;
            }

            internal set
            {
                _visibility_mi = value;
            }
        }

        private Double _visibility_km;
        public Double VisibilityKm
        {
            get
            {
                return _visibility_km;
            }

            internal set
            {
                _visibility_km = value;
            }
        }

        private Int16 _uv;
        public Int16 UV 
        {
            get
            {
                return _uv;
            }

            internal set
            {
                _uv = value;
            }
        }

        /*
                heat_index_f
                heat_index_c

                windchill_f
                windchill_c

                feelslike_f
                feelslike_c
                
                solarradiation??
                
                precip_1hr_in
                precip_1hr_metric

                precip_today_in
                precip_today_metric
                
                icon
                icon_url
                
                forecast_url
                history_url
                ob_url*/
    }

    public class WUndergroudResponseFactory
    {

        public static WUndergroundConditionsResponse CreateWUndergroundConditionsResponse(string response) {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response);

            //Check features
            XmlNode nodeFeatures = doc.SelectSingleNode("/response/features/feature[text() = 'conditions']");
            if (nodeFeatures != null)
            {
                nodeFeatures.ToString();
                WUndergroundConditionsResponse result = new WUndergroundConditionsResponse();

                //Base Result from WUnderground
                result.Version = doc.SelectSingleNode("/response/version").InnerText;
                result.TermOfService = doc.SelectSingleNode("/response/termsofService").InnerText;

                //Base Image for weather
                result.ImageUrl = doc.SelectSingleNode("/response/current_observation/image/url").InnerText;

                //Station Id
                result.StationId = doc.SelectSingleNode("/response/current_observation/station_id").InnerText;

                //General Weather (Should be in link with the Base image for weather)
                result.Weather = doc.SelectSingleNode("/response/current_observation/weather").InnerText;

                //temperature
                result.TemperatureF = Int32.Parse(doc.SelectSingleNode("/response/current_observation/temp_f").InnerText);
                result.TemperatureC = Int32.Parse(doc.SelectSingleNode("/response/current_observation/temp_c").InnerText);

                //Humidity
                result.RelativeHumidity = doc.SelectSingleNode("/response/current_observation/relative_humidity").InnerText;

                //Wind (Include direction, speed and gust speed)
                result.WindDirection = doc.SelectSingleNode("/response/current_observation/wind_dir").InnerText;
                result.WindDegrees = Int16.Parse(doc.SelectSingleNode("/response/current_observation/wind_degrees").InnerText);
                result.WindSpeed_Mph = Int32.Parse(doc.SelectSingleNode("/response/current_observation/wind_mph").InnerText);
                result.WindGust_Mph = Int32.Parse(doc.SelectSingleNode("/response/current_observation/wind_gust_mph").InnerText);
                result.WindSpeed_Kph = Int32.Parse(doc.SelectSingleNode("/response/current_observation/wind_kph").InnerText);
                result.WindGust_Kph = Int32.Parse(doc.SelectSingleNode("/response/current_observation/wind_gust_kph").InnerText);

                //Pressure
                result.Pressure_mb = Int32.Parse(doc.SelectSingleNode("/response/current_observation/pressure_mb").InnerText);

                //TODO MAKE CONVERSION GOOD fwith english version language
                //result.Pressure_in = Double.Parse(doc.SelectSingleNode("/response/current_observation/pressure_in").InnerText);

                result.PressureTrend = doc.SelectSingleNode("/response/current_observation/pressure_trend").InnerText;

                result.DewPointF = Int32.Parse(doc.SelectSingleNode("/response/current_observation/dewpoint_f").InnerText);
                result.DewPointC = Int32.Parse(doc.SelectSingleNode("/response/current_observation/dewpoint_c").InnerText);


                /* 
                heat_index_f
                heat_index_c

                windchill_f
                windchill_c

                feelslike_f
                feelslike_c
                */

                /*
                visibility_mi
                visibility_km
                
                solarradiation??
                
                 */

                result.UV = Int16.Parse(doc.SelectSingleNode("/response/current_observation/UV").InnerText);
                
                /*
                precip_1hr_in
                precip_1hr_metric

                precip_today_in
                precip_today_metric
                */

                /*
                icon
                icon_url
                
                forecast_url
                history_url
                ob_url
                 */

                return result;
            }


            return null;
        }

    }
}
