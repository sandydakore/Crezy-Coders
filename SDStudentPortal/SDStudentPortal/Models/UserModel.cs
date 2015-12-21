﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDStudentPortal.Models
{
    public class UserModel
    {
        [Required]
        public int UserModelID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public MyEnum UserEmailPrivacySetting { get; set; }
        public string ProfilePictureURL { get; set; }
        public MyEnum ProfilePictureURLPrivacySetting { get; set; }
        [Required]
        public int ClassNumber { get; set; }
        [Required]
        public string ProfileName { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Address { get; set; }
        public MyEnum AddressPrivacySetting { get; set; }
        public int PhoneNumber { get; set; }
        public MyEnum PhoneNumberPrivacySetting { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public MyEnum DateOfBirthPrivacySetting { get; set; }
        public string Skills { get; set; }
        public MyEnum SkillsPrivacySetting { get; set; }
        public string Certificates { get; set; }
        public MyEnum CertificatesPrivacySetting { get; set; }
        public string Memberships { get; set; }
        public MyEnum MembershipsPrivacySetting { get; set; }
        public string Experience { get; set; }
        public MyEnum ExperiencePrivacySetting { get; set; }

    }
}