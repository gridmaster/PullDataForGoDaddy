using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using PullDataForGoDaddy.Models;

namespace PullDataForGoDaddy
{
    class Program
    {
        private static string BaseUri = @"http://tickersymbol.info";  // @"http://localhost:45667"; //
        private static string LoadSectorUri = @"/LoadSectors";
        private static string LoadDailySectorUri = @"/LoadDailySectors";
        private static string LoadIndustryUri = @"/LoadIndustries";
        private static string LoadDailyIndustryUri = @"/LoadDailyIndustries";

        private static void Main(string[] args)
        {
            /*
            var shite = GetWebData(BaseUri + "/Industries"); // "/Person?Id=1");
            shite = shite.Substring(1);
            shite = shite.Substring(0, shite.Length - 1);
            var crap = shite.Replace("\\\"", "\"");
            var spit = JsonConvert.DeserializeObject<List<Industry>>(crap);

            var moreshite = xPost<Person>(@"http://tickersymbol.info/" + "Person?Id=1", new Person());
            */

            LoadDailySectors();
            LoadDailyIndustries();
            
            // LoadSectors();

           //  LoadIndustries();

            //var jsonData = JsonConvert.SerializeObject(load);

            //jsonData = jsonData.Replace("T00:00:00", "");

            //// sr.token = "bc2afdc0-6f68-497a-9f6c-4e261331c256";

            //Regex regex = new Regex(@"\b(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})\b"); //@"\d{4}-\d{2}-\d{2}"); //
            //Match snot = regex.Match(jsonData);

            //var beer = MDYToDMY(jsonData);

            //SectorRequest sr = new SectorRequest
            //{
            //    sectors = WcfSector, // .ToList(), // load.ToList(), // 
            //    token = "bc2afdc0-6f68-497a-9f6c-4e261331c256"
            //};

            //var result = Post(uri, sr);

        }

        public static string GetWebData(string uri)
        {
            string webData = string.Empty;

            try
            {
                using (WebClient client = new WebClient())
                {
                    webData = client.DownloadString(uri);
                    webData = Regex.Replace(webData, @"[^\u0000-\u007F]", string.Empty).Replace("&", "&amp;");
                }
            }
            catch (Exception ex)
            {
                //Log.WriteLog(string.Format("Error reading XML. Error: {0}", ex.Message));
            }

            return webData;
        }

        private static string LoadDailySectors()
        {
            string uri = BaseUri + LoadDailySectorUri;

            string result = string.Empty;

            BasicRequest br = new BasicRequest
                {
                    token = "bc2afdc0-6f68-497a-9f6c-4e261331c256"
                };

            result = Post(uri, br);
            
            return result;
        }

        private static string LoadSectors()
        {
            PullDataContext db = new PullDataContext();
            string uri = BaseUri + LoadSectorUri;

            var today = DateTime.Now.ToShortDateString();
            var yesterday = DateTime.Now.AddDays(-1);
            //if (db.Sectors.Any(p => p.Date > yesterday))
            //{

            //}

            string result = string.Empty;
            int start = 0;
            int end = 100;

            List<Sector> load = new List<Sector>();
            do
            {
                load = db.Sectors
                           .Where(x => x.Id > start)
                           .Where(x => x.Id < end)
                           .ToList();

                List<WCFSector> WcfSector = load.Select(item => new WCFSector(item)).ToList();

                SectorRequest sr = new SectorRequest
                {
                    sectors = WcfSector,
                    token = "bc2afdc0-6f68-497a-9f6c-4e261331c256"
                };

               // result = Post(uri, sr);

                start += 100;
                end += 100;
            } while (load.Count > 0);

            return result;
        }

        private static string LoadIndustries()
        {
            PullDataContext db = new PullDataContext();
            string uri = BaseUri + LoadIndustryUri;

            var today = DateTime.Now.ToShortDateString();
            var yesterday = DateTime.Now.AddDays(-1);
            //if (db.Industry.Any(p => p.Date > yesterday))
            //{

            //}

            string result = string.Empty;
            int start = 0;
            int end = 100;

            List<Industry> load = new List<Industry>();
            do
            {
                load = db.Industries
                       .Where(x => x.Id > start)
                       .Where(x => x.Id < end)
                       .ToList();

                List<WCFIndustry> WcfIndustry = load.Select(item => new WCFIndustry(item)).ToList();

                IndustryRequest ir = new IndustryRequest
                {
                    industries = WcfIndustry,
                    token = "bc2afdc0-6f68-497a-9f6c-4e261331c256"
                };

                //result = Post(uri, ir);

                start += 100;
                end += 100;
            } while (load.Count > 0);

            return result;
        }


        private static string LoadDailyIndustries()
        {
            PullDataContext db = new PullDataContext();
            string uri = BaseUri + LoadDailyIndustryUri;

            var today = DateTime.Now.ToShortDateString();
            var yesterday = DateTime.Now.AddDays(-1);

            string result = string.Empty;

            BasicRequest br = new BasicRequest
            {
                token = "bc2afdc0-6f68-497a-9f6c-4e261331c256"
            };

            result = Post(uri, br);


            return result;
        }

        private static string MDYToDMY(string input)
        {
            try
            {
                return Regex.Replace(input,
                      @"\b(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})\b",
                      "${month}/${day}/${year}"); //, RegexOptions.None,
            }
            catch (Exception ex) //RegexMatchTimeoutException)
            {
                return input;
            }
        }

        private static T xPost<T>(string uri, T postData)
        {
            string jsonData = string.Empty;
            const string applicationType = "application/json;charset=utf-8"; //  "application/json"; // "application/x-www-form-urlencoded"; // 

            try
            {
                jsonData = JsonConvert.SerializeObject(postData);
                
                jsonData = jsonData.Replace(" 00:00:00", "");

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

                return JsonConvert.DeserializeObject<T>(jsonResponse);
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

        private static string Post<T>(string uri, T postData)
        {
            string jsonData = string.Empty;
            const string applicationType = "application/json;charset=utf-8"; //  "application/json"; // "application/x-www-form-urlencoded"; // 

            try
            {
                jsonData = JsonConvert.SerializeObject(postData);

                jsonData = jsonData.Replace(" 00:00:00", "");

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
