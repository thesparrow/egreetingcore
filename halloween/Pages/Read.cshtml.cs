using halloween.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;


namespace halloween.Pages
{
    public class ReadModel: PageModel
    {
        //Database related - connect to our model 
        [BindProperty]
        public Greetings Greetings { get; set; }

        //Connection to db
        private Database _dbContext { get; set; }

        //Create the database connection through the constructor
        public ReadModel(Database dbContext)
        {
            _dbContext = dbContext;
        }

        /**
         * @param: ID of Contacts  
         *      Extract from DB
         * 
         */
        public IActionResult OnGet(int id = 0)
        {
            if (id > 0)
            {
                Greetings = _dbContext.Greetings.Find(id);
            }
            
            if(Greetings == null)
                return RedirectToPage("Index");

            return Page();
        }

        
    }
}
