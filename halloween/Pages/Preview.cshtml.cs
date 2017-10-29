using halloween.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace halloween.Pages
{
    public class PreviewModel : PageModel
    {
        //BUILD A BRIDGE 
        [BindProperty]
        public Greetings Greetings { get; set; }

        //Connection to db
        private Database _dbContext { get; set; }

        //hey, Create the database connection through the constructor
        public PreviewModel(Database dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet(int ID=0)
        {
            if (ID > 0)
            {
                Greetings = _dbContext.Greetings.Find(ID); 
            }
            //GRAB the record from the database 
            //EXTRACT from query string 
           
        }
    }
}
