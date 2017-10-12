﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace halloween.Pages
{
    public class IndexModel : PageModel
    {
        public string Message = ""; 
        
        //Default: get the request
        public void OnGet()
        {
            Message = "Hi, Anna!";
        }
    }
}
