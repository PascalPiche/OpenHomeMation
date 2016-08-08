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

        private Double _dewPoint_C;
        public Double DewPoint_C
        {
            get
            {
                return _dewPoint_C;
            }

            internal set
            {
                _dewPoint_C = value;
            }
        }

        private Double _dewPoint_F;
        public Double DewPoint_F
        {
            get
            {
                return _dewPoint_F;
            }

            internal set
            {
                _dewPoint_F = value;
            }
        }

        private Double _feelsLike_C;
        public Double FeelsLike_C
        {
            get
            {
                return _feelsLike_C;
            }
            internal set
            {
                _feelsLike_C = value;
            }
        }
       
        private Double _feelsLike_F;
        public Double FeelsLike_F 
        {
            get 
            {
                return _feelsLike_F;
            }
            internal set 
            {
                _feelsLike_F = value;
            }
        }

        private String _heatIndex_C;
        public String HeatIndex_C
        {
            get
            {
                return _heatIndex_C;
            }
            internal set
            {
                _heatIndex_C = value;
            }
        }

        private String _heatIndex_F;
        public String HeatIndex_F
        {
            get
            {
                return _heatIndex_F;
            }
            internal set
            {
                _heatIndex_F = value;
            }
        }

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

        private Double _pressure_mb;
        public Double Pressure_mb
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

        private Double _temperatureC;
        public Double Temperature_C
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

        private Double _temperatureF;
        public Double Temperature_F
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

        private Double _visibility_km;
        public Double Visibility_Km
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

        private Double _visibility_mi;
        public Double Visibility_Mi
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

        private Double? _windchillF;
        public Double? Windchill_F
        {
            get
            {
                return _windchillF;
            }

            internal set
            {
                _windchillF = value;
            }
        }

        private Double? _windchillC;
        public Double? Windchill_C
        {
            get
            {
                return _windchillC;
            }

            internal set
            {
                _windchillC = value;
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

        private Double _windGust_Kph;
        public Double WindGust_Kph
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

        private Double _windGust_Mph;
        public Double WindGust_Mph
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
        
        private Double _windSpeed_Kph;
        public Double WindSpeed_Kph
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

        private Double _windSpeed_Mph;
        public Double WindSpeed_Mph
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



        
        /*

                

               
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

                //StationNode Id
                result.StationId = doc.SelectSingleNode("/response/current_observation/station_id").InnerText;

                //General Weather (Should be in link with the Base image for weather)
                result.Weather = doc.SelectSingleNode("/response/current_observation/weather").InnerText;

                //temperature
                result.Temperature_F = Double.Parse(doc.SelectSingleNode("/response/current_observation/temp_f").InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                result.Temperature_C = Double.Parse(doc.SelectSingleNode("/response/current_observation/temp_c").InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);

                //Humidity
                result.RelativeHumidity = doc.SelectSingleNode("/response/current_observation/relative_humidity").InnerText;

                //Wind (Include direction, speed and gust speed)

                result.Windchill_F = GetNullableDouble(doc.SelectSingleNode("/response/current_observation/windchill_f").InnerText);
                result.Windchill_C = GetNullableDouble(doc.SelectSingleNode("/response/current_observation/windchill_c").InnerText);
                result.WindDirection = doc.SelectSingleNode("/response/current_observation/wind_dir").InnerText;
                result.WindDegrees = Int16.Parse(doc.SelectSingleNode("/response/current_observation/wind_degrees").InnerText);
                result.WindSpeed_Mph = Double.Parse(doc.SelectSingleNode("/response/current_observation/wind_mph").InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                result.WindGust_Mph = Double.Parse(doc.SelectSingleNode("/response/current_observation/wind_gust_mph").InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                result.WindSpeed_Kph = Double.Parse(doc.SelectSingleNode("/response/current_observation/wind_kph").InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                result.WindGust_Kph = Double.Parse(doc.SelectSingleNode("/response/current_observation/wind_gust_kph").InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);

                //Pressure
                result.Pressure_mb = Double.Parse(doc.SelectSingleNode("/response/current_observation/pressure_mb").InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                result.Pressure_in = Double.Parse(doc.SelectSingleNode("/response/current_observation/pressure_in").InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                result.PressureTrend = doc.SelectSingleNode("/response/current_observation/pressure_trend").InnerText;

                result.DewPoint_F = Double.Parse(doc.SelectSingleNode("/response/current_observation/dewpoint_f").InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                result.DewPoint_C = Double.Parse(doc.SelectSingleNode("/response/current_observation/dewpoint_c").InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);

                result.HeatIndex_F = doc.SelectSingleNode("/response/current_observation/heat_index_f").InnerText;
                result.HeatIndex_C = doc.SelectSingleNode("/response/current_observation/heat_index_c").InnerText;

                

                /* 

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

        public static Double? GetNullableDouble(string value)
        {
            Double parsedValue;

            if (Double.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out parsedValue))
            {
                return new Double?(parsedValue);
            }

            return new Double?();
        }
    }
}
