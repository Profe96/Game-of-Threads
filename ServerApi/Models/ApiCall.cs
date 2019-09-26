using System.IO;
using System.Net;

namespace ServerApi.Models
{
    public class ApiCall
    {
        public static string getRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.ContentType = "application/json";

            var content = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            return content;
        }
    }
}