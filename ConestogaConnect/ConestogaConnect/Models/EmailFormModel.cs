using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConestogaConnect.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail = "sandeepkaur452@gmail.com";
        [Required]
        public string Message { get; set; }
    }
}