using System.ComponentModel.DataAnnotations;

namespace halloween.Models
{
    public class Greetings
    {

        //HEY, Add a unique identifier 
        [Key]
        public int ID { get; set; }
       
        [Display(Name = "Send To", Prompt = "eg Jane")]
        [Required(ErrorMessage="This field is required.")]
        [StringLength(100, MinimumLength =3, ErrorMessage ="Name should be greater than 3, less than a 100.")]
        public string ToName { get; set; }

        [Display(Name = "Send To Email", Prompt="example@email.com")]
        [Required(ErrorMessage = "This field is required.")]
        public string ToEmail { get; set; }


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

        [Display(Name = "Terms and Conditions")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please agree to terms and condition")]
        public bool TermsAndConditions { get; set; }

        //[Required(ErrorMessage = "This field is required.")]
        public string CreateDate { get; set; }

        public string reCaptcha { get; set; }

        //[Required(ErrorMessage = "This field is required.")]
        public string CreateIP { get; set; }
       
        public string SendDate { get; set; }
        public string SendIP { get; set; }

    }
}
