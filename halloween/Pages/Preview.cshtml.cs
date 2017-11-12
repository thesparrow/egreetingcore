using halloween.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        //Create the database connection through the constructor
        public PreviewModel(Database dbContext)
        {
            _dbContext = dbContext;
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

                try
                {
                    // SEND 
                    // Create message
                    MailMessage mailer = new MailMessage();

                    //add toEmail, FromEmail, Subject, Message
                    mailer.To.Add(new MailAddress(Greetings.ToEmail, Greetings.ToName));
                    mailer.From = new MailAddress(Greetings.FromEmail, Greetings.FromEmail); 
                    mailer.Subject = Greetings.Subject;
                    mailer.Body = "";

                    mailer.IsBodyHtml = true;

                    //SEND using SMTP {outgoing server} 
                    // POP incoming 
                    using (var smtpClient = new SmtpClient())
                    {
                        //SmtpClient client = new SmtpClient("mysmtpserver");
                        smtpClient.EnableSsl = true;
                        smtpClient.Host = "smtp.wowoco.org"; //CHANGE 
                        smtpClient.Port = 25;  //
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Send(mailer); 
                    }

                    // DB Related Customized values added with each record
                    Greetings.CreateDate = DateTime.Now.ToString();
                    Greetings.CreateIP = this.HttpContext.Connection.RemoteIpAddress.ToString();

                    // DB Related update record
                    _dbContext.Greetings.Update(Greetings);
                    _dbContext.SaveChanges();

                    return RedirectToPage("ThankYou");
                }
                catch
                {
                    ErrorMessage = "Not able to send your message at this time. Please try again later.";
                }
            }
            return Page();
        }
    }
}
