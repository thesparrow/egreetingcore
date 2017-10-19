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
        [DisplayName("Send To Name")]
        [Display(Prompt = "First Name")] //this is the placeholder 
        [Required(ErrorMessage="This field is required.")]
        [StringLength(100, MinimumLength =3, ErrorMessage ="Name should be greater than 3, less than a 100.")]
        public string SendTo { get; set; }

        [DisplayName("Send To Email")]
        [Required(ErrorMessage = "This field is required.")]
        public string SendersEmail { get; set; }

        [DisplayName("From")]
        public string FromName { get; set; }

        [DisplayName("Email")]
        public string FromEmail { get; set; }

        [DisplayName("Subject")]
        [Required(ErrorMessage = "This field is required.")]
        public string Subject { get; set; }

        [DisplayName("Personalized Message")]
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
