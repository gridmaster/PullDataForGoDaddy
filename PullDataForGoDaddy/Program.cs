using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using PullDataForGoDaddy.Models;

namespace PullDataForGoDaddy
{
    class Program
    {
        private static string BaseUri = @"http://localhost:45667"; //@"http://tickersymbol.info";  // 
        private static string LoadSectorUri = @"/LoadSectors";

        private static void Main(string[] args)
        {
            PullDataContext db = new PullDataContext();

            var today = DateTime.Now.ToShortDateString();
            var yesterday = DateTime.Now.AddDays(-1);
            //if (db.Sectors.Any(p => p.Date > yesterday))
            //{
                
            //}

            var load = db.Sectors.Take(1);

            var jsonData = JsonConvert.SerializeObject(load);
            string uri = BaseUri + LoadSectorUri;
            // sr.token = "bc2afdc0-6f68-497a-9f6c-4e261331c256";

            SectorRequest sr = new SectorRequest
                {
                    sectors = load.ToList(),
                    token = "bc2afdc0-6f68-497a-9f6c-4e261331c256"
                };


            var result = Post(uri, sr);

        }


        private static string Post<T>(string uri, T postData)
        {
            string jsonData = string.Empty;
            const string applicationType = "application/json"; // "application/x-www-form-urlencoded"; // 

            try
            {
                jsonData = JsonConvert.SerializeObject(postData);
                var vuci = JsonConvert.DeserializeObject<SectorRequest>(jsonData);

                byte[] requestData = Encoding.UTF8.GetBytes(jsonData);

                WebRequest request = WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = applicationType;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(requestData, 0, requestData.Length);
                dataStream.Dispose();

                string jsonResponse = string.Empty;
                using (WebResponse response = request.GetResponse())
                {
                    if (((HttpWebResponse)response).StatusDescription == "OK")
                    {
                        dataStream = response.GetResponseStream();
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            jsonResponse = reader.ReadToEnd();
                        }
                    }
                }

                //BasicResponseData responseCode = JsonConvert.DeserializeObject<BasicResponseData>(jsonResponse);
                //if (responseCode.error > 0)
                //{
                //    throw new Exception(string.Format("Error CodeString {0} Returned from Web Service call.",
                //                                      responseCode.error));
                //}

                return jsonResponse;
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "Exception in postAsync{0}URI: {1}{0}Post Data: {2}{0}Content Type: {3}{0}",
                    Environment.NewLine,
                    uri,
                    jsonData,
                    applicationType);

                throw new Exception(error, ex);
            }
        }

    }
}
