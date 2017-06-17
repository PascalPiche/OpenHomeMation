using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using WUnderground.Api.Data;

namespace WUnderground.Api
{
    public class WUndergroundApi
    {
        private const string _baseUrl = "http://api.wunderground.com/api/";

        public static bool QueryLocationExist(string key, int zip, int magic, string wmo)
        {
            string result = Query(key, "conditions", CreateZMW(zip, magic, wmo));
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            return (doc.GetElementsByTagName("current_observation").Count == 1);
        }

        public static WUndergroundConditionsResponse QueryConditions(string key, int zip, int magic, string wmo)
        {
            WUndergroundConditionsResponse result = null;

            try
            {
                string queryResult = Query(key, "conditions", CreateZMW(zip, magic, wmo));
                result = WUndergroudResponseFactory.CreateWUndergroundConditionsResponse(queryResult);
            }
            catch (Exception)
            {
                //TODO PASS EXCEPTION HAS A RESULT
                //throw;
            }
            return result; 
        }

        private static string CreateZMW(int zip, int magic, string wmo)
        {
            return zip.ToString("00000") + "." + magic + "." + wmo;
        }

        private static string Query(string key, string type, string zmw)
        {
            string url = _baseUrl + key + "/" + type + "/q/zmw:" + zmw + ".xml";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                if (errorResponse != null)
                {
                    using (Stream responseStream = errorResponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                        String errorText = reader.ReadToEnd();
                    }
                }
                throw;
            }
        }
    }
}
