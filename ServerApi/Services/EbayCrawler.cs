using System;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

using ServerApi.Models;
using System.Collections.Generic;
using System.Net;

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
            caracteristicas.Add("power");

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
                        caracteristicas2.Add(item + ":" + (prueba3.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("</td>", "").Replace("</tr>", "").Replace(" ", "")).Trim());
                    }
                    else
                    {
                        caracteristicas2.Add(item + ":" + "None");
                    }
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e);
                    caracteristicas2.Add(item + ":" + "None");
                }
            }
            return caracteristicas2;
        }
    }
}