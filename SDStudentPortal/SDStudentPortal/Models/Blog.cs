using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDStudentPortal.Models
{
    public class Blog
    {
        public int BlogID { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BlogCreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BlogUpdatedDate { get; set; }

        public bool Public { get; set; }
    }
}