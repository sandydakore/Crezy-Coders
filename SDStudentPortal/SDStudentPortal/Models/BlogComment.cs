using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDStudentPortal.Models
{
    public class BlogComment
    {
        public int BlogCommentID { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CommentDate { get; set; }

        public int BlogID { get; set; }

        [ForeignKey("BlogID")]
        public virtual Blog blog { get; set; }
    }
}