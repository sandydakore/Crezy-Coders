using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SDStudentPortal.Models
{
    public class Blog
    {
        public int BlogID { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required,AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BlogCreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BlogUpdatedDate { get; set; }

        public MyEnum BlogPrivacySetting { get; set; }
    }
}