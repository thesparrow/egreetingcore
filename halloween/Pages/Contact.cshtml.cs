using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace halloween.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; } //property of the model 

        [BindProperty] //get property from http request
        public string to_name { get; set; }

        [BindProperty]
        public string to_email { get; set; }

        public void OnGet()
        {
          
        }

        public void OnPost()
        {
            Message = "Hello";

        }
    }
}
