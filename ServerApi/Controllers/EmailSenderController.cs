using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ServerApi.Services;
using ServerApi.Models;
using ServerApi.Database;

namespace ServerApi.Controllers
{
    [Route("send/")]
    [ApiController]
    public class EmailSenderController : Controller
    {
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            Dictionary<string, int> vea = new Dictionary<string, int>();
            Connection _c = new Connection();
            var davidsisisisisisisissisito = new Object();
            var users = _c.getUsers();
            Parallel.ForEach(users, (user) =>
            {
                var a = _c.selectUserRecommendations(user.id);
                if (a.Count > 0)
                {
                    IList<Product> products = new EbayApi().getProductsForRecommendation(a);
                    Dictionary<string, string> links = new Dictionary<string, string>();

                    foreach (var productitos in products)
                    {
                        links.Add(productitos.link, productitos.imageUrl);
                        lock (davidsisisisisisisissisito)
                        {
                            if (vea.ContainsKey(productitos.link))
                            {
                                vea[productitos.link]++;
                            }
                            else
                            {
                                vea.Add(productitos.link, 1);
                            }
                        }
                    }
                    sendEmail(user.email, links);
                }
            });
            return vea;
        }

        private void sendEmail(string emails, Dictionary<string, string> linkes)
        {
            var fromEmail = "GameOT2019@gmail.com";
            var fromAddress = new MailAddress(fromEmail, "Game of Threads");
            string fromPassword = "got2019si";
            const string subject = "Recomendaciones para vos";
            var body = "Hola,<br/>Estas son algunas recomendaciones para vos:<br/>";
            int i = 1;
            foreach (KeyValuePair<string, string> entry in linkes)
            {
                body += " Producto " + i++ + " : <img src=\"" + entry.Value + "\"/><br/>" + entry.Key + "<br/>";
            }
            body += "<br/>Felizes fiestas.";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            var toAddress = new MailAddress(emails, "Dear visitor");
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            message.IsBodyHtml = true;

            {
                smtp.Send(message);
            }
        }
    }
}