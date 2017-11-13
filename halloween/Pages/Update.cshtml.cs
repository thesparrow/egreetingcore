using halloween.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace halloween.Pages
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
         * @param: ID of Contacts  
         *      Extract from DB
         * 
         */
        public IActionResult OnGet(int ID = 0)
        {
            if (ID > 0)
            {
                Greetings = _dbContext.Greetings.Find(ID);
                return Page();
            }

            else
            {
                return RedirectToPage("Index"); 
            }
        }


        public string ErrorMessage { get; set; }
        /** 
         *  @param: Contact ID 
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