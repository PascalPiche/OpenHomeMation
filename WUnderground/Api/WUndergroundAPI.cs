using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WUnderground.Api
{
    public class WUndergroundApi
    {
        private const string _baseUrl = "http://api.wunderground.com/api/";

        public static bool QueryLocationExist(int zip, int magic, int wmo)
        {
            string result = Query("condition", zip + "." + magic + "." + wmo);
            return false;
        }

        

        private static string Query(string type, string zmw)
        {
            string url = _baseUrl + "q/" + type + "/zmw:" + zmw + ".json";
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
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
    }
}
