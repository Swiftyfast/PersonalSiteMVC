using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalSiteMVC.Models;
using System.Net.Mail;
using System.Net;

namespace PersonalSiteMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }

        //Below is the Logic for the Contact form
        //The GET should be would takes you to that page.
        public ActionResult Contact()
        {
            return View();
        }

        //the POST will be below. But also, I will need
        //X- ContactViewModel.cs
        //Email Confirmation view
        //redo the Contact.cshtml
        //Do the Post which will be below.

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            //Steps to send an email:
            //1) Create a string fot he message
            string emailBody = $"You have received an email from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response the following message: <br /><br /> {cvm.Message}";
            //2) Create the MailMessage object - System.Net.Mail
            MailMessage msg = new MailMessage
            (
                //From
                "no-reply@johndavidswift.com",
                //To(where the actual message is sent)
                "jdavidswift@gmail.com",
                //Subject
                "Email from johndavidswift.com",
                //Body
                emailBody
            );
            //3) (optional) Customize the MailMessage Object
            msg.IsBodyHtml = true; //Allow HTML formatting in the email
            //msg.CC.Add("anotherEmail@email.com");
            //msg.ReplyToList.Add(cvm.Email); //Response to the sender's email instead of our smarterasp.net email
            //4) Create the SmptpClient - This is the information from the HOST (smarterasp.net)
            //This allows the email to actually be sent
            SmtpClient client = new SmtpClient("mail.johndavidswift.com");

            //Client Credentials (SmarterASP requires your username and password)
            client.Credentials = new NetworkCredential("no-reply@johndavidswift.com", "LOL_NOPE");
            client.Port = 8889;

            //5) Attempt to send the email
            //It is always possible the mailserver will be down or may have a configuration issue.
            //So we want to wrap our code with a try/catch
            try
            {
                //atempt to send the email
                client.Send(msg);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Sorry, something went wrong. Error message: {ex.Message}<br />{ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);
        }

    }
}