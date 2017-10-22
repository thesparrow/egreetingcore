using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace halloween.Models
{
    public class Greetings
    {
        //[DisplayName("Send To")]
        //[Display(Prompt = "eg Jane")] //this is the placeholder 
        [Display(Name = "Send To", Prompt = "eg Jane")]
        [Required(ErrorMessage="This field is required.")]
        [StringLength(100, MinimumLength =3, ErrorMessage ="Name should be greater than 3, less than a 100.")]
        public string SendTo { get; set; }

        [Display(Name = "Send To Email", Prompt="example@email.com")]
        [Required(ErrorMessage = "This field is required.")]
        public string SendersEmail { get; set; }


        [Display(Name ="From", Prompt ="eg Best Friend")]
        public string FromName { get; set; }

        [Display(Name ="From Email", Prompt ="example@email.com")]
        [Required(ErrorMessage = "This field is required.")]
        public string FromEmail { get; set; }

        [Display(Name ="Subject", Prompt = "Greeting Subject")]
        [Required(ErrorMessage = "This field is required.")]
        public string Subject { get; set; }

        [Display(Name = "Message", Prompt = "Write your message here!")]
        [Required(ErrorMessage = "This field is required.")]
        public string Message { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string CreateDate { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string CreateIP { get; set; }
       
        public string SendDate { get; set; }
        public string SendIP { get; set; }

    }
}
