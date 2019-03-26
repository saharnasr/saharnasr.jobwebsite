using Jobs.Offer.Wbesite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Jobs.Offer.Wbesite.Models
{
    public class Job
    {
        public int Id { get; set; }
        [DisplayName("إسم الوظيفه")]
        public string JobTitle { get; set; }
        [DisplayName("وصف الوظيفه")]
        [AllowHtml]
        public string JobContent { get; set; }
        [DisplayName("صورة الوظيفه")]
        public string JobImage { get; set; }

        [DisplayName("نوع الوظيفه")]
        public int CategoryId { get; set; }

        public string UserID { get; set; }


        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}