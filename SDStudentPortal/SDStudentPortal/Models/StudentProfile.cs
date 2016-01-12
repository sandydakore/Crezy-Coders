using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SDStudentPortal.Models
{
    public class StudentProfile
    {
        public int StudentProfileID { get; set; }

        public virtual UserModel User { get; set; }

        public virtual IEnumerable<Project> Projects { get; set; }
        public virtual IEnumerable<Blog> Blogs { get; set; }
    }
}