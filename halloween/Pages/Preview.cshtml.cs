using halloween.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace halloween.Pages
{
    public class PreviewModel : PageModel
    {
        public string ErrorMessage { get; set; }
        //Database related - connect to our model 
        [BindProperty]
        public Greetings Greetings { get; set; }

        //Connection to db
        private Database _dbContext { get; set; }

        private IConfiguration _configruation { get; set; }

        //Create the database connection through the constructor
        public PreviewModel(Database dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configruation = configuration; 
        }
       

        /**
         * @param: ID of Contacts  
         *      Extract from DB
         * 
         */
        public void OnGet(int ID = 0)
        {
            if (ID > 0)
            {
                Greetings = _dbContext.Greetings.Find(ID);
            }
        }

        /** 
         *  @param: Contact ID 
         *      Submit form
         */
        public IActionResult OnPost(int ID = 0)
        {
            if (ID > 0)
            {
                Greetings = _dbContext.Greetings.Find(ID);

                var emailUrl = "http://anna.wowoco.org/emai;?id=" + Greetings.ID;

                try
                {
                    // SEND 
                    // Create message
                    var mailer = new MailMessage();

                    //add toEmail, FromEmail, Subject, Message
                    mailer.To.Add(new MailAddress(Greetings.ToEmail, Greetings.ToName));
                    mailer.From = new MailAddress(Greetings.FromEmail, Greetings.FromEmail); 
                    mailer.Subject = Greetings.Subject;

                    using (WebClient client = new WebClient())
                    {
                        mailer.Body = client.DownloadString(new Uri(emailUrl)); 
                    }

                    mailer.IsBodyHtml = true;

                    //SEND using SMTP {outgoing server} 
                    // POP incoming 
                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        smtpClient.EnableSsl = Boolean.Parse(_configruation["SMTP:EnableSsl"]); 
                        smtpClient.Host = _configruation["SMTP:Host"];   
                        smtpClient.Port = Int32.Parse(_configruation["SMTP:Port"]);
                        smtpClient.UseDefaultCredentials = Boolean.Parse(_configruation["SMTP:UseDefaultCredentials"]);
                        smtpClient.Send(mailer); 
                    }

                    // DB Related Customized values added with each record
                    Greetings.CreateDate = DateTime.Now.ToString();
                    Greetings.CreateIP = this.HttpContext.Connection.RemoteIpAddress.ToString();

                    // DB Related update record
                    _dbContext.Greetings.Update(Greetings);
                    _dbContext.SaveChanges();

                    return RedirectToPage("Complete");
                }
                catch{
                    ErrorMessage = "Not able to send your message at this time. Please try again later.";
                    //ErrorMessage = ex.ToString(); 

                }
            }
            return Page();
        }
    }
}
