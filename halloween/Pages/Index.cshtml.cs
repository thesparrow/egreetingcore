using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using halloween.Models;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace halloween.Pages
{
    public class IndexModel : PageModel
    {

        //BUILD A BRIDGE 
        [BindProperty]
        public Greetings Greetings { get; set; }

        //Connection to db
        private Database _dbContext { get; set; }

        public bool isPreviewPage { get; set; }

        //hey, Create the database connection through the constructor
        public IndexModel(Database dbContext)
        {
            _dbContext = dbContext;
        }

        //DEFAULT LOAD 
        public void OnGet()
        {
            isPreviewPage = false;
        }

        //PREVIEW MODE 
        [HttpPost]
        public async Task<IActionResult> OnPost()
        {
            //call the database 

            if (await isValid())
            {
                if (ModelState.IsValid)
                {
                    //try
                    //{

                        // ADD TO DATABASE through instance of our form
                        _dbContext.Greetings.Add(Greetings);
                        _dbContext.SaveChanges(); 

                        //REDIRECT to the page with a new operator (name/value pair)
                        return RedirectToPage("Preview", new { id = Greetings.ID} );
                    //}

                    //catch { }

                }
            }
            else
            {
                ModelState.AddModelError("Greetings.reCaptcha", "Please verify you're not a robot!");
            }

            return Page();


            // if (ModelState.IsValid)
            //{
            //some action 
            //add to db 
            // speaks to db -> databaseContext 
            //_dbContext.Greetings.Add(Greetings);
            //_dbContext.SaveChanges();

            //return RedirectToPage("Preview", new { id = Greetings.ID });
            //}


            //return RedirectToPage("Index");

        }

        //reCAPTHCA SERVER SIDE VALIDATION  
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
