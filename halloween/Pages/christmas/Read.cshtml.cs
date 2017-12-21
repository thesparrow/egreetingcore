using halloween.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace halloween.Pages.christmas
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

        public IActionResult OnGet(int ID = 0)
        {

            if (ID > 0)
            {
                Greetings = _dbContext.Greetings.Find(ID);
            }
            
            if(Greetings == null)
                return RedirectToPage("Index");

            return Page();
        }

        
    }
}
