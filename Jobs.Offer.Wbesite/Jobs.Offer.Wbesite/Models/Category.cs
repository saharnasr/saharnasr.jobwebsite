using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jobs.Offer.Wbesite.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("نوع الوظيفة")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("وصف النوع")]
        public string CategoryDescription { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }

    }
}