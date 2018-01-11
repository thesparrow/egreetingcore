using egreeting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace egreeting.Pages.valentine
{
    public class UpdateModel : PageModel
    {

        //Database related - connect to our model 
        [BindProperty]
        public Greetings Greetings { get; set; }

        //Connection to db
        private Database _dbContext { get; }


        //Create the database connection through the constructor
        public UpdateModel(Database dbContext)
        {
            _dbContext = dbContext;
        }

        /**
         * @param: id of Contacts  
         *      Extract from DB
         * 
         */
        public IActionResult OnGet(int id = 0)
        {
            if (id > 0)
            {
                Greetings = _dbContext.Greetings.Find(id);
                return Page();
            }

            else
            {
                return RedirectToPage("Index"); 
            }
        }


        public string ErrorMessage { get; set; }
        /** 
         *  @param: Contact id 
         *      Submit form
         */
        public IActionResult OnPost()
        {
            try
            {
                // DB Related update record
                _dbContext.Greetings.Update(Greetings);
                _dbContext.SaveChanges();

                return RedirectToPage("Preview", new {ID=Greetings.ID});
            }
            catch
            {
                ErrorMessage = "Not able to send your message at this time. Please try again later.";
            }

            return Page();
        }
    }
}