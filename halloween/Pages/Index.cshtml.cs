using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using halloween.Models;

namespace halloween.Pages
{
    public class IndexModel : PageModel
    {

        //BUILD A BRIDGE 
        [BindProperty]
        public Greetings bridgeGreetings { get; set; }


        public bool isPreviewPage { get; set; }


        //DEFAULT LOAD 
        public void OnGet()
        {
            isPreviewPage = false; 
        }

        //PREVIEW MODE 
        [HttpPost]
        public void OnPost()
        {
            isPreviewPage = true;
        }

      
    
        //public bool isPreviewPage { get; set; }
        //public string validationMessage { get; set; }

        ////BINDING PROPERTIES
        //[BindProperty]
        //public ContactForm ContactForm { get; set; }

        ////Default: FORM INTIALLY LOADS
        //public void OnGet()
        //{
        //    isPreviewPage = false;
        //    validationMessage = ""; 
        //}

        ////FORM IS SUBMITTED 
        //public void OnPost()
        //{
        //    isPreviewPage = true;

        //    if (string.IsNullOrEmpty(ContactForm.ToName))
        //    {
        //        validationMessage = "Please enter who you are sending to";
        //        isPreviewPage = false; 
        //    }
        //}


    }
}
