using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace SDStudentPortal.Models
{
    public class ProjectFile
    {
        public int ProjectFileID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string UploadFileUrl { get; set; }

        public virtual int ProjectID { get; set; }
        public virtual Project project { get; set; }
    }
}