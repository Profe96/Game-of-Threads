using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ServerApi.Models;

namespace ServerApi.Services
{
    public class EbayCrawler
    {
        public static List<string> crawlerForDescription(string url)
        {
            WebClient client = new WebClient();
            String htmlCode = client.DownloadString(url);
            List<string> caracteristicas = new List<string>();
            caracteristicas.Add("color");
            caracteristicas.Add("brand");
            caracteristicas.Add("size");

            List<string> caracteristicas2 = new List<string>();

            foreach (var item in caracteristicas)
            {
                try
                {
                    var copia = htmlCode.ToLower();
                    var prueba = "";
                    var prueba2 = "";
                    var prueba3 = "";

                    if (htmlCode.ToLower().Contains(item))
                    {
                        prueba = copia.Substring(0, copia.IndexOf(">" + item + "</td>"));
                        prueba2 = copia.Replace(prueba, "");
                        prueba2 = prueba2.Substring(prueba2.IndexOf("</td>"), prueba2.IndexOf("</tr>"));
                        prueba3 = prueba2.Replace(prueba2.Substring(prueba2.IndexOf("</td>"), prueba2.IndexOf("\">") + 2), "");
                        caracteristicas2.Add(item + ":" + (prueba3.Replace("\r\n", "")
                                .Replace("\n", "").Replace("\r", "")
                                .Replace("</td>", "").Replace("</tr>", "")
                                .Replace("<td>", "").Replace("<tr>", "")
                                .Replace(" ", "")).Replace("\t", "")
                            .Replace("<trclas", "").Trim());
                    }
                    else
                    {
                        caracteristicas2.Add(item + ":" + "none");
                    }
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e);
                    caracteristicas2.Add(item + ":" + "none");
                }
            }
            return caracteristicas2;
        }
    }
}