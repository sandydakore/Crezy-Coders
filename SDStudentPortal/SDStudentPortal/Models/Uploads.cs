using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SDStudentPortal.Models
{
    public class Uploads
    {
        public int UploadsID { get; set; }

        [ForeignKey("User")]
        public virtual string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string UploadFileUrl { get; set; }
        public MyEnum UploadFileUrlPrivacySetting { get; set; }
        public string FileType { get; set; }

        public virtual int ProjectID { get; set; }
        public virtual Project project { get; set; }
    }
}