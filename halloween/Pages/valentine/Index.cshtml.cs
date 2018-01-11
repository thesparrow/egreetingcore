using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using egreeting.Models;
using Microsoft.Extensions.Configuration;

namespace egreeting.Pages.valentine
{
    public class IndexModel: PageModel
    {

        //BUILD A BRIDGE 
        [BindProperty]
        public Greetings Greetings { get; set; }

        //Connection to db
        private Database _dbContext { get; set; }

        public bool isPreviewPage { get; set; }

        private IConfiguration _configuration { get; set; }

        //hey, Create the database connection through the constructor
        public IndexModel(Database dbContext)
        {
            _dbContext = dbContext;
        }

        //DEFAULT LOAD 
        public void OnGet()
        {
            
        }

        /**
         * PREVIEW MODE
         * 
         *      On form submission: clean the form data, 
         *      insert into db, redirect to Preview mode
         */
        [HttpPost]
        public async Task<IActionResult> OnPost()
        {

            if (await isValid())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // DB Related Customized values added with each record
                        Greetings.CreateDate = DateTime.Now.ToString();
                        Greetings.CreateIP = this.HttpContext.Connection.RemoteIpAddress.ToString();

                        //Clean Data before insertion 
                        Greetings.FromEmail = Greetings.FromEmail.ToLowerInvariant();
                        Greetings.ToEmail = Greetings.ToEmail.ToLowerInvariant();

                        // DB Related add record
                        _dbContext.Greetings.Add(Greetings);
                        _dbContext.SaveChanges(); 

                        //REDIRECT to the page with a new operator (name/value pair)
                        return RedirectToPage("Preview", new { id = Greetings.ID} );
                    }

                    catch(Exception ex) {
                        Console.WriteLine(ex); 
                        return RedirectToPage("Index");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Greetings.reCaptcha", "Please verify you're not a robot!");
            }

            return Page();

        }

        /**
         * reCAPTHCA SERVER SIDE VALIDATION 
         * 
         *      Create an HttpClient and store the the secret/response pair
         *      Await for the sever to return a json obect 
         * */
        private async Task<bool> isValid()
        {
            var response = this.HttpContext.Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(response))
                return false;

            try
            {
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>();
                    values.Add("secret", "6LfVpjEUAAAAAK0FdygAgh0P1gZ8QU24ildwT86r");
                    values.Add("response", response);
                    //values.Add("remoteip", this.HttpContext.Connection.RemoteIpAddress.ToString()); 

                    var query = new FormUrlEncodedContent(values);

                    var post = client.PostAsync("https://www.google.com/recaptcha/api/siteverify", query);

                    var json = await post.Result.Content.ReadAsStringAsync();

                    if (json == null)
                        return false;

                    var results = JsonConvert.DeserializeObject<dynamic>(json);

                    return results.success;
                }

            }
            catch { }

            return false;
        }

    }
}
