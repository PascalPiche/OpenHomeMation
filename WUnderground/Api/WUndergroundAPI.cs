﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WUnderground.Api.Data;


namespace WUnderground.Api
{
    public class WUndergroundApi
    {
        private const string _baseUrl = "http://api.wunderground.com/api/";

        public static bool QueryLocationExist(string key, int zip, int magic, int wmo)
        {
            string result = Query(key, "conditions", CreateZMW(zip, magic, wmo));
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            return (doc.GetElementsByTagName("current_observation").Count == 1);
        }

        public static WUndergroundConditionsResponse QueryConditions(string key, int zip, int magic, int wmo)
        {
            return WUndergroudResponseFactory.CreateWUndergroundConditionsResponse(Query(key, "conditions", CreateZMW(zip, magic, wmo)));
        }

        private static string CreateZMW(int zip, int magic, int wmo)
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
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
            catch (IOException ex)
            {

                throw;
            }
        }
    }
}
