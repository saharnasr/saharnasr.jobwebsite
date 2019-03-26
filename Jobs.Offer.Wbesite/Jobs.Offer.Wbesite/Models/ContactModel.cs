using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jobs.Offer.Wbesite.Models
{
    public class ContactModel
    {
        [Required]
        [Display(Name = "الإسم")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "البريد الإلكترونى")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "عنوان الرسالة")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "محتوى الرسالة")]
        public string Message { get; set; }



    }
}