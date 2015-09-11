using CrystalVortex.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CrystalVortex.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Contact(ContactModel c)
        {
            if (ModelState.IsValid)
            {
                var emailTo = new MailAddress(ConfigurationManager.AppSettings["ContactEmail"].ToString());
                var emailFrom = new MailAddress(ConfigurationManager.AppSettings["SmtpFrom"].ToString());
                var smtpHost = ConfigurationManager.AppSettings["SmtpHost"].ToString();

                var client = new SmtpClient(smtpHost);
                var msg = new MailMessage();
                msg.From = emailFrom;
                msg.ReplyToList.Add(new MailAddress(c.EmailAddress));
                msg.To.Add(emailTo);
                msg.Subject = "Crystal Vortex Feedback";
                msg.Body = String.Format("Message From: {0} {1}\nMessage Submitted {2}\n\n{3}", c.FirstName, c.LastName, DateTime.UtcNow.ToString("R"), c.Comments);
                msg.IsBodyHtml = false;

                await client.SendMailAsync(msg);

                return View("ContactSubmitted");
            }

            return View();
        }
    }
}