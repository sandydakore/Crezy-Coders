using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDStudentPortal.Models
{
    public class UserModel
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int UserEmailPrivacySetting { get; set; }
        [Required]
        public string ProfilePictureURL { get; set; }
        public int ProfilePictureURLPrivacySetting { get; set; }
        [Required]
        public int ClassNumber { get; set; }
        [Required]
        public string ProfileName { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Address { get; set; }
        public int AddressPrivacySetting { get; set; }
        public int PhoneNumber { get; set; }
        public int PhoneNumberPrivacySetting { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DateOfBirthPrivacySetting { get; set; }
        public string Skills { get; set; }
        public int SkillsPrivacySetting { get; set; }
        public string Certificates { get; set; }
        public int CertificatesPrivacySetting { get; set; }
        public string Memberships { get; set; }
        public int MembershipsPrivacySetting { get; set; }
        public string Experience { get; set; }
        public int ExperiencePrivacySetting { get; set; }

    }
}