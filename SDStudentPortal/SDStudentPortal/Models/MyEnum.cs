using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDStudentPortal.Models
{
    //Privacy Settings
    //    Public = 0
    //    UsersOnly = 1
    //    Private = 2
    public enum MyEnum
    {
        [Display(Name = "Public")]
        Public = 0,
        [Display(Name = "UsersOnly")]
        UsersOnly = 1,
        [Display(Name = "Private")]
        Private = 2
    }
}